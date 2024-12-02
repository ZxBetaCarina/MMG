﻿using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserDetails : MonoBehaviour
{
    [SerializeField] private Image pic;
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField number;
    [SerializeField] private TMP_InputField dob;
    [SerializeField] private TMP_InputField refer;
    [SerializeField] private TMP_InputField location;
    [SerializeField] private ToggleGroup gender;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button back;
    [SerializeField] private Button editPic;

    private string _selectedImagePath;
    
    public static event Action OnDetailsSubmit;

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinue);
        back.onClick.AddListener(OnBack);
        dob.onValueChanged.AddListener(FormatDateInput);
        editPic.onClick.AddListener(OnEditPic);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(OnContinue);
        back.onClick.RemoveListener(OnBack);
        dob.onValueChanged.RemoveListener(FormatDateInput);
        editPic.onClick.RemoveListener(OnEditPic);
    }

    private void OnEditPic()
    {
        PickImage(1024); // Max image size (adjust as needed)
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
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
        string cleanedInput = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");
        if (cleanedInput.Length > 8)
        {
            cleanedInput = cleanedInput.Substring(0, 8);
        }

        string formattedInput = "";
        for (int i = 0; i < cleanedInput.Length; i++)
        {
            if (i == 2 || i == 4)
            {
                formattedInput += "/";
            }
            formattedInput += cleanedInput[i];
        }

        dob.text = formattedInput;
        dob.caretPosition = dob.text.Length;
    }

    private void OnContinue()
    {
        if (firstName.text == String.Empty || lastName.text == String.Empty || number.text == String.Empty ||
            dob.text == String.Empty || location.text == String.Empty || !gender.AnyTogglesOn())
        {
            PopUpManager.ShowPopUp("Message", "Please fill all the fields and select Gender");
        }
        else
        {
            OnDetailsSubmit?.Invoke();
            
            
            var form = new WWWForm();
            form.AddField("firstName", firstName.text);
            form.AddField("lastName", lastName.text);
            form.AddField("whatsappNumber", number.text);
            form.AddField("dob", dob.text);
            form.AddField("referNumber", refer.text);
            form.AddField("location", location.text);
            form.AddField("gender", gender.GetFirstActiveToggle().name);

         if (!string.IsNullOrEmpty(_selectedImagePath))
        {
            // Load the image from the path
            Texture2D texture = NativeGallery.LoadImageAtPath(_selectedImagePath, 1024); // Adjust the max size as needed
            if (texture != null)
            {
                // Resize and compress the image
                texture = ResizeTexture(texture, 800); // Resize to a max dimension of 800px (adjust as needed)
                byte[] imageBytes = texture.EncodeToJPG(75); // Compress the image (75% quality for JPG)

                // Check if the file size is acceptable (1MB in this case)
                if (imageBytes.Length > 1024 * 1024)
                {
                    PopUpManager.ShowPopUp("Message", "The image is too large, please choose a smaller one.");
                    return;
                }

                // Add the image to the form as binary data
                form.AddBinaryData("profileImage", imageBytes, "profileImage.jpg", "image/jpeg");
            }
            else
            {
                PopUpManager.ShowPopUp("Message", "Could not load the image.");
                return;
            }
        }

        // Send the form to the server
        ApiManager.PostForm<UserDataResponse>(ServiceURLs.UpdateProfile, form, OnSuccessUpdateUserData, OnErrorUpdateUserData);
    }
}

private Texture2D ResizeTexture(Texture2D original, int maxDimension)
{
    // Maintain the aspect ratio while resizing
    float aspectRatio = (float)original.width / original.height;
    int newWidth = original.width;
    int newHeight = original.height;

    if (original.width > original.height)
    {
        newWidth = maxDimension;
        newHeight = Mathf.RoundToInt(newWidth / aspectRatio);
    }
    else
    {
        newHeight = maxDimension;
        newWidth = Mathf.RoundToInt(newHeight * aspectRatio);
    }

    // Create a new texture with the resized dimensions
    Texture2D resizedTexture = new Texture2D(newWidth, newHeight);
    Graphics.ConvertTexture(original, resizedTexture); // Copy the pixels
    return resizedTexture;
}
    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            PopUpManager.ShowPopUp("Message", "Welcome To Millionaire Mind Games");
            UIManager.LoadScreenAnimated(UIScreen.Home);
        }
    }

    private void OnErrorUpdateUserData(string obj)
    {
        Debug.Log("Error updating profile: " + obj);
    }

    private void OnBack()
    {
      
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }
}

public class UserDataResponse
{
    public bool status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
}

public class UserDataRequest
{
    public UserDataRequest(string firstName, string lastName, string whatsappNumber, string dob, string referNumber,
        string location, string gender, string profileImage)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.whatsappNumber = whatsappNumber;
        this.dob = dob;
        this.referNumber = referNumber;
        this.location = location;
        this.gender = gender;
        this.profileImage = profileImage;
    }

    public string firstName { get; set; }
    public string lastName { get; set; }
    public string whatsappNumber { get; set; }
    public string dob { get; set; }
    public string referNumber { get; set; }
    public string location { get; set; }
    public string gender { get; set; }
    public string profileImage { get; set; }
}