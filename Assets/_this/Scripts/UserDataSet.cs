using System;
using UnityEngine;
using ZxLog;

public class UserData : MonoBehaviour
{
    [SerializeField] private Data data = new();
    [SerializeField] private Sprite pic;
    [SerializeField] private Sprite qRcode;
    [SerializeField] private AppSettings appSettings;
    private static UserData _instance;
    private string key = "token";

    private void Awake()
    {
      
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public static bool IsUserLoggedIn { get; private set; }

    private void OnValidate()
    {
        if (appSettings == null)
        {
            appSettings = FindObjectOfType<AppSettings>(true);
        }
    }

    private void OnEnable()
    {
        UserDetails.OnDetailsSubmit += () => SetLogIn(true);
        Otp.OnOtpVerified += () => SetLogIn(true);
        Account.OnSignOutAction += () => SetLogIn(false);
        UserDetails.OnDetailsSubmit += SaveDataOnSystem;
        Otp.OnOtpVerified += SaveDataOnSystem;
    }

    private void OnDisable()
    {
        UserDetails.OnDetailsSubmit -= () => SetLogIn(true);
        Otp.OnOtpVerified -= () => SetLogIn(true);
        Account.OnSignOutAction -= () => SetLogIn(false);
        UserDetails.OnDetailsSubmit -= SaveDataOnSystem;
        Otp.OnOtpVerified -= SaveDataOnSystem;
    }
    private void SaveDataOnSystem()
    {
        if (data.token != null)
        {
            PlayerPrefs.SetString(key, data.token);
            Print.CustomLog("Token is saved for autologin save", LogColor.Cyan);
        }
        else
        {
            Print.CustomLog("Token is null for autologin save", LogColor.Cyan);
        }
    }

    public static string GetKey()
    {
        return _instance.key;
    }

    public static string GetData(UserDataSet dataType)
    {
        switch (dataType)
        {
            case UserDataSet.Email:
                return UserData._instance.data.email;
            case UserDataSet.Number:
                return UserData._instance.data.whatsappNumber;
            case UserDataSet.FirstName:
                return UserData._instance.data.firstName;
            case UserDataSet.LastName:
                return UserData._instance.data.lastName;
            case UserDataSet.Dob:
                return UserData._instance.data.dob;
            case UserDataSet.Gender:
                return UserData._instance.data.gender;
            case UserDataSet.Otp:
                return UserData._instance.data.otp;
            case UserDataSet.Id:
                return UserData._instance.data._id;
            case UserDataSet.ReferNumber:
                return UserData._instance.data.referNumber;
            case UserDataSet.Location:
                return UserData._instance.data.location;
            case UserDataSet.ProfileImage:
                return UserData._instance.data.profileImage;
            case UserDataSet.QrCode:
                return UserData._instance.data.qrCode;
            case UserDataSet.FcmToken:
                return UserData._instance.data.fcmToken;
            case UserDataSet.DeviceId:
                return UserData._instance.data.deviceId;
            case UserDataSet.SellerId:
                return UserData._instance.data.sellerId;
            case UserDataSet.Token:
                return UserData._instance.data.token;
            default:
                Print.CustomLog("Invalid data type in GetData", LogColor.Red);
                return null;
        }
    }

    private void Update()
    {
        string email = UserData.GetData(UserDataSet.Email);
    
        // Log the email to the console for debugging
        //Debug.Log("User Email: " + email);
    }

    public static void SetData(UserDataSet dataType, string value)
    {
        switch (dataType)
        {
            case UserDataSet.Email:
                UserData._instance.data.email = value;
                break;
            case UserDataSet.Number:
                UserData._instance.data.whatsappNumber = value;
                break;
            case UserDataSet.FirstName:
                UserData._instance.data.firstName = value;
                break;
            case UserDataSet.LastName:
                UserData._instance.data.lastName = value;
                break;
            case UserDataSet.Dob:
                UserData._instance.data.dob = value;
                break;
            case UserDataSet.Gender:
                UserData._instance.data.gender = value;
                break;
            case UserDataSet.Otp:
                UserData._instance.data.otp = value;
                break;
            case UserDataSet.Id:
                UserData._instance.data._id = value;
                break;
            case UserDataSet.ReferNumber:
                UserData._instance.data.referNumber = value;
                break;
            case UserDataSet.Location:
                UserData._instance.data.location = value;
                break;
            case UserDataSet.ProfileImage:
                UserData._instance.data.profileImage = value;
                break;
            case UserDataSet.QrCode:
                UserData._instance.data.qrCode = value;
                break;
            case UserDataSet.FcmToken:
                UserData._instance.data.fcmToken = value;
                break;
            case UserDataSet.DeviceId:
                UserData._instance.data.deviceId = value;
                break;
            case UserDataSet.SellerId:
                UserData._instance.data.sellerId = value;
                break;
            case UserDataSet.Token:
                UserData._instance.data.token = value;
                break;
            default:
                Print.CustomLog("Invalid data type in SetData", LogColor.Red);
                break;
        }
        _instance.appSettings.GetSettingsFromUserData();
    }

    public static void SetTotalData(Data data)
    {
        _instance.data = data;
    }

    public static Data GetTotalData()
    {
        return _instance.data;
    }

    private void SetLogIn(bool value)
    {
        IsUserLoggedIn = value;
        PlayerPrefs.SetInt("IsUserLoggedIn", IsUserLoggedIn ? 1 : 0);
        SignOut(value);
    }

    private void OnApplicationQuit()
    {
        IsUserLoggedIn = false;
        PlayerPrefs.SetInt("IsUserLoggedIn", IsUserLoggedIn ? 1 : 0);
    }

    private void SignOut(bool value)
    {
        if (!value)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static void SetSettings(Settings settings)
    {
        _instance.data.settings = settings;
    }

    public static Settings GetSettings()
    {
        return _instance.data.settings;
    }

    public static Sprite GetImage()
    {
        return _instance.pic;
    }


    public static void SetImage(Sprite image)
    {
        _instance.pic = image;
    }

    public static Sprite GetQrCode()
    {
        return _instance.qRcode;
    }
    
    public static void SetQrImage(Sprite image)
    {
        _instance.qRcode = image;
    }
}

public enum UserDataSet
{
    Email,
    Number,
    FirstName,
    LastName,
    Dob,
    Gender,
    Otp,
    Id,
    ReferNumber,
    Location,
    ProfileImage,
    QrCode,
    FcmToken,
    DeviceId,
    SellerId,
    Token,
}