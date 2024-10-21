using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZxLog;

public class Otp : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] otpFields;
    [SerializeField] private GameObject invalidText;
    [SerializeField] private Button verify;
    [SerializeField] private Button resend;
    [SerializeField] private Button back;

    public static event Action OnOtpVerified;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        resend.onClick.AddListener(OnResend);
        verify.onClick.AddListener(OnVerify);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        resend.onClick.RemoveListener(OnResend);
        verify.onClick.RemoveListener(OnVerify);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }

    private void OnResend()
    {
        ApiManager.Post<SignInRequestData, SignInResponseData>(ServiceURLs.Login, new SignInRequestData(UserData
                .GetData(UserDataSet.Email)),
            OnSuccessResendOtp, OnErrorResendOtp);
    }

    private void OnSuccessResendOtp(SignInResponseData obj)
    {
        if (obj.status)
        {
            ResetAllFields();
            UIManager.ShowPopUp("Message", "OTP Resent successfully");
        }
        else
        {
            CustomLog.ErrorLog(obj.message + obj.status);
        }
    }

    private void OnErrorResendOtp(string obj)
    {
        CustomLog.ErrorLog(obj);
    }


    private void OnVerify()
    {
        ApiManager.Post<OtpRequestData, ProfileResponseData>(ServiceURLs.VerifyOtp, new OtpRequestData(UserData.GetData
            (UserDataSet.Email), GetOtpFieldsData().ToString()), OnSuccessVerifyOtp, OnErrorVerifyOtp);
    }

    private void OnSuccessVerifyOtp(ProfileResponseData obj)
    {
        if (obj.status)
        {
            UserData.SetTotalData(obj.data);
            OnOtpVerified?.Invoke();
            if (UserData.GetData(UserDataSet.Token) != String.Empty)
            {
                ApiManager.SetAuthToken(UserData.GetData(UserDataSet.Token));
            }
            else
            {
                CustomLog.ErrorLog("Token Missing from userdata");
                return;
            }

            NextScreen();
        }
        else
        {
            CustomLog.ErrorLog(obj.message + obj.status);
        }
    }

    private void OnErrorVerifyOtp(string obj)
    {
        CustomLog.ErrorLog(obj);
        invalidText.SetActive(true);
    }

    private static void NextScreen()
    {
        if (UserData.GetTotalData().isProfileComplete)
        {
            UIManager.LoadScreenAnimated(UIScreen.Home);
        }
        else
        {
            UIManager.LoadScreenAnimated(UIScreen.UserDetails);
        }
    }

    void Start()
    {
        TouchScreenKeyboard.Android.consumesOutsideTouches = false;
        for (int i = 0; i < otpFields.Length; i++)
        {
            int index = i;
            otpFields[i].onValueChanged.AddListener(delegate { OnFieldValueChanged(index); });
        }
    }

    void OnFieldValueChanged(int index)
    {
        if (otpFields[index].text.Length == 1 && index < otpFields.Length - 1)
        {
            //otpFields[index + 1].Select();
            otpFields[index + 1].ActivateInputField();
        }

        if (otpFields[index].text == "" && index > 0)
        {
            //otpFields[index - 1].Select();
            otpFields[index - 1].ActivateInputField();
        }
    }

    void ResetAllFields()
    {
        foreach (TMP_InputField field in otpFields)
        {
            field.text = "";
        }
    }

    private int GetOtpFieldsData()
    {
        var otp = "";
        foreach (var value in otpFields)
        {
            otp += value.text;
        }

        Print.BigWhiteLog("OTP = " + otp);
        return int.Parse(otp);
    }
}

public class OtpRequestData
{
    public string email;
    public string otp;

    public OtpRequestData(string email, string otp)
    {
        this.email = email;
        this.otp = otp;
    }
}

[Serializable]
public class ProfileResponseData
{
    public bool status;
    public string message;
    public Data data;
}

[Serializable]
public class Data
{
    public Settings settings;
    public string _id;
    public string firstName;
    public string lastName;
    public string email;
    public string otp;
    public bool otpVerified;
    public string whatsappNumber;
    public string dob;
    public string referNumber;
    public string location;
    public string gender;
    public string profileImage;
    public string qrCode;
    public bool active;
    public bool isProfileComplete;
    public string fcmToken;
    public string deviceId;

    public string sellerId;

    //public DateTime createdAt ;
    //public DateTime updatedAt ;
    public int __v;
    public string token;
}

[Serializable]
public class Settings
{
    public bool music;
    public bool soundEffect;
    public bool vibration;
}