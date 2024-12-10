using System;
using System.Threading.Tasks;
using UnityEngine;

public class GetWalletApi
{
    private static int _earnedPoints;
    private static int _gamingPoints;

    private static void GetWallet(Action<(string, string)> onSuccess, Action<string> onError)
    {
        ApiManager.Get<GetWalletResponseData>(ServiceURLs.GetWallet,
            (GetWalletResponseData obj) =>
            {
                OnSuccessGetWallet(obj);
                onSuccess?.Invoke((_earnedPoints.ToString(), _gamingPoints.ToString()));
            },
            onError);
    }

    private static void OnSuccessGetWallet(GetWalletResponseData obj)
    {
        if (obj.status)
        {
            Debug.Log(obj.message);
            _earnedPoints = obj.data.earnedPoints;
            _gamingPoints = obj.data.gamingPoints;
        }
    }

    private static void OnErrorGetWallet(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    /// <summary>
    /// Get Wallet Amount From Server Directly with a callback.
    /// </summary>
    /// <returns>
    /// item1 = Earned Points,
    /// item2 = Gaming Points
    /// </returns>
    public static void GetWalletAmount(Action<(string, string)> onSuccess, Action<string> onError)
    {
        GetWallet(onSuccess, onError);
    }
}

public class GetWalletResponseData
{
    public bool status;
    public string message;
    public WalletData data;
}

public class WalletData
{
    public string _id;
    public int gamingPoints;
    public int earnedPoints;
    public int bonusPoints;
}