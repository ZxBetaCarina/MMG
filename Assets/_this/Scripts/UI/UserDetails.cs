using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserDetails : MonoBehaviour
{
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField number;
    [SerializeField] private TMP_InputField dob;
    [SerializeField] private TMP_InputField refer;
    [SerializeField] private TMP_InputField location;
    [SerializeField] private ToggleGroup gender;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button back;

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinue);
        back.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(OnContinue);
        back.onClick.RemoveListener(OnBack);
    }

    private void OnContinue()
    {
        //check for all fields are filled
        if (firstName.text == String.Empty || lastName.text == String.Empty || number.text == String.Empty ||
            dob.text == String.Empty || refer.text == String.Empty || location.text == String.Empty ||
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
        form.AddField("referNumber", refer.text);
        form.AddField("location", location.text);
        form.AddField("gender", gender.GetFirstActiveToggle().name);
        form.AddField("profileImage", "");
        return form;
    }


    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            UIManager.ShowPopUp("Message", "Welcome To Millionaire Mind Games");
            UIManager.LoadScreenAnimated(UIScreen.Home);
            CustomLog.SuccessLog(obj.message);
        }
    }

    private void OnErrorUpdateUserData(string obj)
    {
        CustomLog.ErrorLog(obj);
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