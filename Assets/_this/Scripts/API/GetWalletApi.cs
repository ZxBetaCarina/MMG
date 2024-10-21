using System;
using UnityEngine;

public class GetWalletApi
{
    public static event Action<int, int> GetPoints;
    public static void GetWallet()
    {
        ApiManager.Get<GetWalletResponseData>(ServiceURLs.GetWallet, OnSuccessGetWallet, OnErrorGetWallet);
    }

    private static void OnSuccessGetWallet(GetWalletResponseData obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.message);
            GetPoints?.Invoke(obj.data.earnedPoints, obj.data.gamingPoints);
        }
    }

    private static void OnErrorGetWallet(string obj)
    {
        CustomLog.ErrorLog(obj);
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
}