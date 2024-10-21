using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        done.onClick.AddListener(OnDone);
        editPic.onClick.AddListener(OnEditPic);
        backBtt.onClick.AddListener(OnBack);
        email.text = UserData.GetData(UserDataSet.Email);
    }

    private void OnDisable()
    {
        done.onClick.RemoveListener(OnDone);
        editPic.onClick.RemoveListener(OnEditPic);
        backBtt.onClick.RemoveListener(OnBack);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserProfile);
    }

    private void OnDone()
    {
        //check for all fields are filled
        if (firstName.text == String.Empty || lastName.text == String.Empty || number.text == String.Empty ||
            dob.text == String.Empty || location.text == String.Empty ||
            !gender.AnyTogglesOn())
        {
            UIManager.ShowPopUp("Message", "Please fill all the fields and select Gender");
            return;
        }
        else
        {
            var formData = CreateForm();
            ApiManager.PostForm<UserDataResponse>(ServiceURLs.UpdateProfile, formData, OnSuccessUpdateUserData,
                OnErrorUpdateUserData);
        }
    }

    private WWWForm CreateForm()
    {
        var form = new WWWForm();
        form.AddField("firstName", firstName.text);
        form.AddField("lastName", lastName.text);
        form.AddField("whatsappNumber", number.text);
        form.AddField("dob", dob.text);
        form.AddField("referNumber", "");
        form.AddField("location", location.text);
        form.AddField("gender", gender.GetFirstActiveToggle().name);
        if (_selectedImagePath != string.Empty)
        {
            form.AddField("profileImage", _selectedImagePath);
        }
        return form;
    }


    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            UIManager.ShowPopUp("Message", "Profile Data Changed Successfully");
            UIManager.LoadScreenAnimated(UIScreen.UserProfile);
            CustomLog.SuccessLog(obj.message);
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
}