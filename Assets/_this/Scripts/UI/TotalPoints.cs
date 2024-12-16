using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZxLog;

public class TotalPoints : MonoBehaviour
{
    public static TotalPoints instance;
    
    [Header("Points Data")]
    public int gamePoints; // Default points value
    public int earnedPoints; // Default earned points
    public int BonusPoints; // Default earned points

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //UpdateWalletPoints();
        }

        if (BonusPoints != 0 && BonusPointsTimer._instance.IsTimeExpired() == true)
        {
            BonusPoints = 0;
            UpdateWalletPoints();
            Debug.Log("Bonus points reset to 0 because the timer expired.");
        }
    }

    public void SetGamePoints(int points)
    {
        gamePoints = points;
    }
    public void SetBonusPoints(int points)
    {
        BonusPoints = points;
    }

    public void SetEarnedPoints(int points)
    {
        earnedPoints = points;
    }
    
    public void GetWallet()
    {
        ApiManager.Get<GetWalletResponseData>(ServiceURLs.GetWallet, OnSuccessGetWallet, OnErrorGetWallet);
    }

    private void OnSuccessGetWallet(GetWalletResponseData obj)
    {
        if (obj.status)
        {
            Debug.Log(obj.status + obj.message);
            gamePoints = obj.data.gamingPoints;
            earnedPoints = obj.data.earnedPoints;
            BonusPoints = obj.data.bonusPoints;
        }
    }

    private void OnErrorGetWallet(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
/// <summary>
/// ///////////////////////////////////
/// </summary>
    public void UpdateWalletPoints()
    {
        var data = new UpdateWalletRequest(gamePoints, earnedPoints, BonusPoints);
        ApiManager.Post<UpdateWalletRequest, UpdateWalletResponse>(ServiceURLs.UpdateWallet, data, OnSuccesUpdate, OnErrorUpdate);
    }

    private void OnSuccesUpdate(UpdateWalletResponse obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.status + obj.message);
        }
    }

    private void OnErrorUpdate(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
    
}
/// <summary>
/// ///////////////////////
/// </summary>
public class UpdateWalletRequest
{
    public int gamingPoints;
    public int earnedPoints;
    public int bonusPoints;
    public UpdateWalletRequest(int gamingPoints,int earnedPoints,int bonusPoints)
    {
        this.gamingPoints = gamingPoints;
        this.earnedPoints = earnedPoints;
        this.bonusPoints = bonusPoints;
    }
}
public class UpdateWalletResponse
{
    public bool status;
    public string message;
    public UpdateWalletData UpdateWalletData;
}

public class UpdateWalletData
{
    public string _id { get; set; }
    public string gameUserId { get; set; }
    public int gamingPoints { get; set; }
    public int earnedPoints { get; set; }
    public int bonusPoints { get; set; }
    public List<TransactionHistory> transactionHistory { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}
public class TransactionHistory
{
    public string transactionType { get; set; }
    public string pointType { get; set; }
    public int points { get; set; }
    public string description { get; set; } 
    public string _id { get; set; }
    public DateTime date { get; set; }
}

