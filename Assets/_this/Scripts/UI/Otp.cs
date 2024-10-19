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
    }

    private static void NextScreen()
    {
        OnOtpVerified.Invoke();
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    void Start()
    {
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
            otpFields[index + 1].ActivateInputField(); // Keeps the keyboard open while moving focus
        }

        if (otpFields[index].text == "" && index > 0)
        {
            otpFields[index - 1].ActivateInputField(); // Keeps the keyboard open when backspacing
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

public class OtpResponseData
{
    public bool status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
}

[Serializable]
public class Data
{
    public Settings settings { get; set; }
    public string _id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string otp { get; set; }
    public bool otpVerified { get; set; }
    public string whatsappNumber { get; set; }
    public string dob { get; set; }
    public string referNumber { get; set; }
    public string location { get; set; }
    public string gender { get; set; }
    public string profileImage { get; set; }
    public string qrCode { get; set; }
    public bool active { get; set; }
    public bool isProfileComplete { get; set; }
    public string fcmToken { get; set; }
    public string deviceId { get; set; }
    public string sellerId { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
    public string token { get; set; }
}

[Serializable]
public class Settings
{
    public bool music { get; set; }
    public bool soundEffect { get; set; }
    public bool vibration { get; set; }
}