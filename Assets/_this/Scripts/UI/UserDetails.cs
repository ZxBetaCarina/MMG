using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZxLog;

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
            dob.text == String.Empty || location.text == String.Empty ||
            !gender.AnyTogglesOn())
        {
            UIManager.ShowPopUp("Message", "Please fill all the fields and select Gender");
        }
        else
        {
            var data = new UserDataRequest(firstName.text, lastName.text, number.text, dob.text, refer.text,
                location.text, gender.GetFirstActiveToggle().name, "");
            ApiManager.Post<UserDataRequest, UserDataResponse>(ServiceURLs.UpdateProfile, data, OnSuccessUpdateUserData,
                OnErrorUpdateUserData);
        }
    }


    private void OnSuccessUpdateUserData(UserDataResponse obj)
    {
        if (obj.status)
        {
            UIManager.ShowPopUp("Message", "Welcome To Millionaire Mind Games");
            UIManager.LoadScreenAnimated(UIScreen.Home);
            CustomLog.SuccessLog(obj.message);
            Profile.GetProfile();

            Print.Separator(LogColor.Yellow);
            Print.CustomLog(obj.data.firstName, LogColor.Yellow);
            Print.Separator(LogColor.Yellow);
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