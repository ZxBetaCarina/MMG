﻿using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserDetails : MonoBehaviour
{
    [SerializeField] private Image pic;
    [SerializeField]private Sprite defaultPic;
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
    [SerializeField] private Sprite Profilesprite;
    private string _selectedImagePath=null;
    
    public static event Action OnDetailsSubmit;

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        continueButton.onClick.AddListener(OnContinue);
        back.onClick.AddListener(OnBack);
        dob.onValueChanged.AddListener(FormatDateInput);
        editPic.onClick.AddListener(OnEditPic);
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed = OnBack;
        continueButton.onClick.RemoveListener(OnContinue);
        back.onClick.RemoveListener(OnBack);
        dob.onValueChanged.RemoveListener(FormatDateInput);
        editPic.onClick.RemoveListener(OnEditPic);
    }

    private void OnEditPic()
    {
        PickImage(512,2); // Max image size (adjust as needed)
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
        else if (_selectedImagePath == null)
        {
            PopUpManager.ShowPopUp("Message", "Please select a profile picture");
            
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

           
           
             // byte[] imageBytes = File.ReadAllBytes(_selectedImagePath);
             // form.AddBinaryData("profileImage", imageBytes, Path.GetFileName(_selectedImagePath), "image/png");
             if(!string.IsNullOrEmpty(_selectedImagePath))
             {
                 byte[] imageBytes = File.ReadAllBytes(_selectedImagePath);
                 form.AddBinaryData("profileImage", imageBytes, Path.GetFileName(_selectedImagePath), "image/png"); 
             }
             
             // else
             // {
             //     Debug.Log("calling in here");
             //     Texture2D  selectedTexture =Profilesprite.texture;
             //     byte[] imageBytes = selectedTexture.EncodeToPNG(); // or EncodeToJPG() if you prefer JPG format
             //     form.AddBinaryData("profileImage", imageBytes, "profileImage.png", "image/png");
             // }
           //  Texture2D  selectedTexture = pic.sprite.texture;
           // byte[] imageBytes = selectedTexture.EncodeToPNG(); // or EncodeToJPG() if you prefer JPG format
           //  form.AddBinaryData("profileImage", imageBytes, "profileImage.png", "image/png");
            ApiManager.PostForm<UserDataResponse>(ServiceURLs.UpdateProfile, form, OnSuccessUpdateUserData,
                OnErrorUpdateUserData);
        }
    }
    
    // string GetDefaltImagePath()
    // {
    //     string path = Path.Combine(Application.streamingAssetsPath, "Sprites/Person Image_1.png");
    //     if (File.Exists(path))
    //     {
    //         Debug.Log("Sprite path: " + path);
    //     }
    //     else
    //     {
    //         Debug.Log("Sprite not found at path: " + path);
    //     }
    //     return path;
    // }
    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            PopUpManager.ShowPopUp("Message", "Welcome To Millionaire Mind Games");
            UIManager.LoadScreenAnimated(UIScreen.Home);
            CustomLog.SuccessLog(obj.message);
            Profile.GetProfile();
            TrailPeriod.instance.Settrailperiod();
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