using UnityEngine;
using ZxLog;

public class CustomLog : MonoBehaviour
{
    public static void ErrorLog(string msg)
    {
        Print.Separator(LogColor.Red);
        Print.CustomLog("Error = " + msg, LogColor.Red);
        Print.Separator(LogColor.Red);
    }

    public static void SuccessLog(string msg)
    {
        Print.Separator(LogColor.Green);
        Print.CustomLog("Success = " + msg, LogColor.Green);
        Print.Separator(LogColor.Green);
    }

    public static void ShowData(string msg)
    {
        Print.Separator(LogColor.Blue);
        Print.CustomLog("Data = " + msg, LogColor.Blue);
        Print.Separator(LogColor.Blue);
    }
}