using UnityEngine;

public class UserData : MonoBehaviour
{
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

    private void SetLogIn(bool value)
    {
        IsUserLoggedIn = value;
    }
}