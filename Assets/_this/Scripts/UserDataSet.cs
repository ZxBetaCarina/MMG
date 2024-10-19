using UnityEngine;
using ZxLog;

public class UserData : MonoBehaviour
{
    [SerializeField] private string email;
    [SerializeField] private string number;
    [SerializeField] private string firstName;
    [SerializeField] private string lastName;
    [SerializeField] private string dob;
    [SerializeField] private string gender;

    private static UserData _instance;
    public static bool IsUserLoggedIn { get; private set; }

    private void OnEnable()
    {
        Otp.OnOtpVerified += () => SetLogIn(true);
        Account.OnSignOutAction += () => SetLogIn(false);
    }

    private void OnDisable()
    {
        Otp.OnOtpVerified -= () => SetLogIn(true);
        Account.OnSignOutAction -= () => SetLogIn(false);
    }

    public static string GetData(UserDataSet dataType)
    {
        switch (dataType)
        {
            case UserDataSet.Email:
                return UserData._instance.email;
            case UserDataSet.Number:
                return UserData._instance.number;
            case UserDataSet.FirstName:
                return UserData._instance.firstName;
            case UserDataSet.LastName:
                return UserData._instance.lastName;
            case UserDataSet.Dob:
                return UserData._instance.dob;
            case UserDataSet.Gender:
                return UserData._instance.gender;
            default:
                Print.CustomLog("Invalid data type in GetData", LogColor.Red);
                return null;
        }
    }

    public static void SetData(UserDataSet dataType, string value)
    {
        switch (dataType)
        {
            case UserDataSet.Email:
                UserData._instance.email = value;
                break;
            case UserDataSet.Number:
                UserData._instance.number = value;
                break;
            case UserDataSet.FirstName:
                UserData._instance.firstName = value;
                break;
            case UserDataSet.LastName:
                UserData._instance.lastName = value;
                break;
            case UserDataSet.Dob:
                UserData._instance.dob = value;
                break;
            case UserDataSet.Gender:
                UserData._instance.gender = value;
                break;
            default:
                Print.CustomLog("Invalid data type in SetData", LogColor.Red);
                break;
        }
    }

    private void SetLogIn(bool value)
    {
        IsUserLoggedIn = value;
    }
}

public enum UserDataSet
{
    Email,
    Number,
    FirstName,
    LastName,
    Dob,
    Gender
}