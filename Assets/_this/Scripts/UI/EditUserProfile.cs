using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZxLog;

public class EditUserProfile : MonoBehaviour
{
    [SerializeField] private Image pic;
    [SerializeField] private TMP_Text email;
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField number;
    [SerializeField] private TMP_InputField dob;
    [SerializeField] private TMP_InputField location;
    [SerializeField] private ToggleGroup gender;
    [SerializeField] private Button backBtt;
    [SerializeField] private Button editPic;
    [SerializeField] private Button done;

    private string _selectedImagePath;

    private void OnEnable()
    {
        pic.sprite = UserData.GetImage();
        done.onClick.AddListener(OnDone);
        editPic.onClick.AddListener(OnEditPic);
        backBtt.onClick.AddListener(OnBack);
        email.text = UserData.GetData(UserDataSet.Email);
        dob.onValueChanged.AddListener(FormatDateInput);
        
        // Add listeners to validate input
        firstName.onValueChanged.AddListener(ValidateNameInput);
        lastName.onValueChanged.AddListener(ValidateLastNameInput);
        location.onValueChanged.AddListener(ValidateLocationInput);
    }

    private void OnDisable()
    {
        done.onClick.RemoveListener(OnDone);
        editPic.onClick.RemoveListener(OnEditPic);
        backBtt.onClick.RemoveListener(OnBack);
        dob.onValueChanged.RemoveListener(FormatDateInput);
        
        // Remove listeners
        firstName.onValueChanged.RemoveListener(ValidateNameInput);
        lastName.onValueChanged.RemoveListener(ValidateLastNameInput);
        location.onValueChanged.RemoveListener(ValidateLocationInput);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserProfile);
    }

    private void OnDone()
    {
        if (firstName.text == String.Empty || lastName.text == String.Empty || number.text == String.Empty ||
            dob.text == String.Empty || location.text == String.Empty ||
            !gender.AnyTogglesOn())
        {
            PopUpManager.ShowPopUp("Message", "Please fill all the fields and select Gender");
        }
        else
        {
            var form = new WWWForm();
            form.AddField("firstName", firstName.text);
            form.AddField("lastName", lastName.text);
            form.AddField("whatsappNumber", number.text);
            form.AddField("dob", dob.text);
            form.AddField("referNumber", "5");
            form.AddField("location", location.text);
            form.AddField("gender", gender.GetFirstActiveToggle().name);
            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(_selectedImagePath);
                form.AddBinaryData("profileImage", imageBytes, Path.GetFileName(_selectedImagePath), "image/png");
            }
            else
            {
                form.AddBinaryData("profileImage", null, Path.GetFileName(_selectedImagePath), "image/png");
            }

            ApiManager.PostForm<UserDataResponse>(ServiceURLs.UpdateProfile, form, OnSuccessUpdateUserData,
                OnErrorUpdateUserData);
        }
    }


    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            PopUpManager.ShowPopUp("Message", "Profile Data Changed Successfully");
            UIManager.LoadScreenAnimated(UIScreen.UserProfile);
            CustomLog.SuccessLog(obj.message);
            Profile.GetProfile();
        }
    }

    private void OnErrorUpdateUserData(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    private void OnEditPic()
    {
        PickImage(5);
    }

    private void PickImage(int maxSize)
    {
        Debug.Log("Picking image...");
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
     
            if (path != null)
            {
                _selectedImagePath = path;
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.LogError("Couldn't load texture from " + path);
                    return;
                }

                pic.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
            }
        });

        Debug.Log("Permission result: " + permission);
    }

    private void FormatDateInput(string input)
    {
        // Remove any non-numeric characters
        string cleanedInput = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");

        // Limit the input to 8 digits
        if (cleanedInput.Length > 8)
        {
            cleanedInput = cleanedInput.Substring(0, 8);
        }

        // Format the input with slashes
        string formattedInput = "";
        for (int i = 0; i < cleanedInput.Length; i++)
        {
            if (i == 2 || i == 4) // Add a slash after the 2nd and 4th digits
            {
                formattedInput += "/";
            }

            formattedInput += cleanedInput[i];
        }

        // Update the input field with the formatted input
        dob.text = formattedInput;

        // Set the caret position at the end of the input
        dob.caretPosition = dob.text.Length;

    }
    private void ValidateNameInput(string input)
    {
        // Remove any numeric characters from the input field's text
        string cleanInput = System.Text.RegularExpressions.Regex.Replace(input, "[^a-zA-Z]", "");
        if (input != cleanInput)
        {
            // Set the cleaned input back to the field
            firstName.text = cleanInput;
            // You can also reset the caret position to the end of the text field
            firstName.caretPosition = cleanInput.Length;
        }
    }
    private void ValidateLastNameInput(string input)
    {
        // Remove any numeric characters from the input field's text
        string cleanInput = System.Text.RegularExpressions.Regex.Replace(input, "[^a-zA-Z]", "");
        if (input != cleanInput)
        {
            // Set the cleaned input back to the field
            lastName.text = cleanInput;
            // You can also reset the caret position to the end of the text field
            lastName.caretPosition = cleanInput.Length;
        }
    }

    private void ValidateLocationInput(string input)
    {
        // Remove any numeric characters from location input
        string cleanInput = System.Text.RegularExpressions.Regex.Replace(input, "[^a-zA-Z\\s]", "");
        if (input != cleanInput)
        {
            // Set the cleaned input back to the location field
            location.text = cleanInput;
            // Reset the caret position
            location.caretPosition = cleanInput.Length;
        }
    }
}