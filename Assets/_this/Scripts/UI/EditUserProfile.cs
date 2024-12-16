﻿using System;
using System.Collections.Generic;
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
        UIManager._onbackbuttonpressed += OnBack;
        GetAllUserData();
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
        UIManager._onbackbuttonpressed -= OnBack;
        done.onClick.RemoveListener(OnDone);
        editPic.onClick.RemoveListener(OnEditPic);
        backBtt.onClick.RemoveListener(OnBack);
        dob.onValueChanged.RemoveListener(FormatDateInput);
        
        // Remove listeners
        firstName.onValueChanged.RemoveListener(ValidateNameInput);
        lastName.onValueChanged.RemoveListener(ValidateLastNameInput);
        location.onValueChanged.RemoveListener(ValidateLocationInput);
    }

    private void GetAllUserData()
    {
        email.text = UserData.GetData(UserDataSet.Email);
        firstName.text = UserData.GetData(UserDataSet.FirstName);
        lastName.text = UserData.GetData(UserDataSet.LastName);
        number.text = UserData.GetData(UserDataSet.Number);
        dob.text = UserData.GetData(UserDataSet.Dob);
        location.text = UserData.GetData(UserDataSet.Location);
        
      //  gender.text = UserData.GetData(UserDataSet.Gender);
      
        string genderData=UserData.GetData(UserDataSet.Gender);
     
        Toggle[] toggles=gender.GetComponentsInChildren<Toggle>();
        
        foreach (Toggle toggle in toggles)
      {
        
          toggle.isOn = toggle.name == genderData;
        
      }
        SetPic();
    }

    private void SetPic()
    {
        pic.sprite = UserData.GetImage();
    }
    

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserProfile);
    }

    private void OnDone()
    {
    // Create a list to store the names of empty fields
    List<string> emptyFields = new List<string>();

    // Check each field for emptiness and add to the emptyFields list
    if (string.IsNullOrEmpty(firstName.text))
        emptyFields.Add("First Name");

    if (string.IsNullOrEmpty(lastName.text))
        emptyFields.Add("Last Name");

    if (string.IsNullOrEmpty(number.text))
        emptyFields.Add("WhatsApp Number");

    if (string.IsNullOrEmpty(dob.text))
        emptyFields.Add("Date of Birth");

    if (string.IsNullOrEmpty(location.text))
        emptyFields.Add("Location");

    if (!gender.AnyTogglesOn())
        emptyFields.Add("Gender");

    // Check if any field is empty
    if (emptyFields.Count > 0)
    {
        // If all fields are empty, show a generic message
        if (emptyFields.Count == 6)
        {
            PopUpManager.ShowPopUp("Message", "Please fill all the fields and select Gender");
        }
        else
        {
            // Otherwise, show a message with the specific empty fields
            string fieldsList = string.Join(", ", emptyFields);
            PopUpManager.ShowPopUp("Message", "Please fill the following fields: " + fieldsList);
        }
    }
    else if (!string.IsNullOrEmpty(dob.text) && dob.text.Length < 10)
    {
        PopUpManager.ShowPopUp("Message", "Please enter a valid Date of Birth in format of (dd/mm/yyyy)");
        return; // Exit the method if the validation fails
    }
    else if (!string.IsNullOrEmpty(number.text) && number.text.Length != 10)
    {
        PopUpManager.ShowPopUp("Message", "Please enter a valid 10-digit WhatsApp number");
        return; // Exit the method if the validation fails
    }
    else
    {
        // Proceed with form submission if everything is filled
        var form = new WWWForm();
        form.AddField("firstName", firstName.text);
        form.AddField("lastName", lastName.text);
        form.AddField("whatsappNumber", number.text);
        form.AddField("dob", dob.text);
        form.AddField("location", location.text);
        form.AddField("gender", gender.GetFirstActiveToggle().name);

        // If no image is selected, use the image from the pic Image component
        if (string.IsNullOrEmpty(_selectedImagePath))
        {
            // Get the sprite from the pic and convert it to a byte array
            if (pic.sprite != null)
            {
                byte[] imageBytes = GetImageBytesFromSprite(pic.sprite);
                form.AddBinaryData("profileImage", imageBytes, "profileImage.png", "image/png");
            }
            else
            {
                PopUpManager.ShowPopUp("Message", "Please select a profile picture");
                return;
            }
        }
        else
        {
            // If an image is selected, send the image from the gallery
            byte[] imageBytes = File.ReadAllBytes(_selectedImagePath);
            form.AddBinaryData("profileImage", imageBytes, Path.GetFileName(_selectedImagePath), "image/png");
        }

        ApiManager.PostForm2<UserDataResponse>(ServiceURLs.UpdateProfile, form, OnSuccessUpdateUserData, OnErrorUpdateUserData);
    }
}
    private byte[] GetImageBytesFromSprite(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        byte[] imageBytes = texture.EncodeToPNG();
        return imageBytes;
    }

    string GetDefaltImagePath()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Sprites/Person Image_1.png");
        if (File.Exists(path))
        {
            Debug.Log("Sprite path: " + path);
        }
        else
        {
            Debug.Log("Sprite not found at path: " + path);
        }
        return path;
    }

    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            PopUpManager.ShowPopUp("Message", "Profile Data Changed Successfully");
            UIManager.LoadScreenAnimated(UIScreen.UserProfile);
           Debug.Log(obj.message);
            Profile.GetProfile();
            
        }
    }

    private void OnErrorUpdateUserData(UserDataResponse obj)
    {
       if (obj.data.whatsAppExists == true)
       {
           PopUpManager.ShowPopUp("Message", "Whatsapp Number Already Exists Please select another Whatsapp Number");
       }
       else
       {
           Debug.Log("Error updating profile: " + obj);
       }
    }

    private void OnEditPic()
    {
        PickImage(512,2);
    }
    private void PickImage(int maxSize, int size)
    {
        Debug.Log("Picking image...");
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
     
            if (path != null)
            {
                // Get the file size of the selected image
                long fileSize = new System.IO.FileInfo(path).Length;

                // Check if the image is larger than 2 MB (2 * 1024 * 1024 bytes)
                if (fileSize > size * maxSize * maxSize)
                {
                    PopUpManager.ShowPopUp("Message", "image size should be less then 2 MB");
                    return;
                }

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
   
    // private void PickImage(int maxSize,int size)
    // {
    //     Debug.Log("Picking image...");
    //     NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
    //     {
    //         Debug.Log("Image path: " + path);
    //  
    //         if (path != null)
    //         {
    //             _selectedImagePath = path;
    //             // Create Texture from selected image
    //             Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
    //             if (texture == null)
    //             {
    //                 Debug.LogError("Couldn't load texture from " + path);
    //                 return;
    //             }
    //
    //             pic.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
    //                 new Vector2(0.5f, 0.5f));
    //         }
    //     });
    //
    //     Debug.Log("Permission result: " + permission);
    // }

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