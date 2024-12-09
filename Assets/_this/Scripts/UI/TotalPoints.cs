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
public int bonuspoint;
    //[Header("UI References")]
    //[SerializeField] private TextMeshProUGUI gamePointsText;
    //[SerializeField] private TextMeshProUGUI earnedPointsText;
    //[SerializeField] private TextMeshProUGUI MainUIPointsTxt;

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
    private void OnEnable()
    {
        //

    }
    private void Update()
    {
        //MainUIPointsTxt.text = gamePoints.ToString();
        //gamePointsText.text = gamePoints.ToString();
        //earnedPointsText.text = earnedPoints.ToString();
        
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     UpdateWalletPoints();
        // }
    }

    public void setbonusPoints(int point)
    {
        bonuspoint = point;
        SetGamePoints(bonuspoint);
        UpdateWalletPoints();
    }
    public void SetGamePoints(int points)
    {
       
        gamePoints = points;
        UpdateWalletPoints();
    }

    public void SetEarnedPoints(int points)
    {
        earnedPoints = points;
        UpdateWalletPoints();
    }

    public void DecreasePoints(int points)
    {
        if (TrailPeriod.instance.isTrailPeriod)
        {
            if (bonuspoint >= points)
            {
                bonuspoint -= points;
                gamePoints -= bonuspoint;
                UpdateWalletPoints();

            }
            else
            {
                points -= bonuspoint;
                gamePoints-= points;
                UpdateWalletPoints();
            }
        }else
        {
            gamePoints -= points;
            UpdateWalletPoints();
        }
        
    }
    
    public void GetWallet()
    {
        ApiManager.Get<GetWalletResponseData>(ServiceURLs.GetWallet, OnSuccessGetWallet, OnErrorGetWallet);
    }

    private void OnSuccessGetWallet(GetWalletResponseData obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.status + obj.message);
            gamePoints = obj.data.gamingPoints;
            earnedPoints = obj.data.earnedPoints;
        }
    }

    private void OnErrorGetWallet(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
/// <summary>
/// ///////////////////////////////////
/// </summary>
    private void UpdateWalletPoints()
    {
        PlayerPrefs.SetInt("BonusPoints", bonuspoint);
        var data = new UpdateWalletRequest(gamePoints, earnedPoints);
        ApiManager.Post<UpdateWalletRequest, UpdateWalletResponse>(ServiceURLs.UpdateWallet, data, OnSuccesUpdate, OnErrorUpdate);
    }

    private void OnSuccesUpdate(UpdateWalletResponse obj)
    {
        if (obj.status)
        {
        bonuspoint=PlayerPrefs.GetInt("BonusPoints");
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
    public UpdateWalletRequest(int gamingPoints,int earnedPoints)
    {
        this.gamingPoints = gamingPoints;
        this.earnedPoints = earnedPoints;
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

