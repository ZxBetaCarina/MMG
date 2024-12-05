using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZxLog;

public class TotalPoints : MonoBehaviour
{
    public static TotalPoints instance;
    
    [Header("Points Data")]
    public int gamePoints = 500; // Default points value
    public int earnedPoints = 300; // Default earned points
    private bool isCredit=true;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI gamePointsText;
    [SerializeField] private TextMeshProUGUI earnedPointsText;

    private const string GamePointsKey = "GamePoints"; // Key for PlayerPrefs
    private const string EarnedPointsKey = "EarnedPoints"; // Key for PlayerPrefs

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

        // Load saved points when the scene starts
        LoadPoints();
    }
    

    private void Start()
    {
        // Initial update for UI
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Updates the points displayed in the UI.
    /// </summary>
    private void UpdatePointsDisplay()
    {
        if (gamePointsText != null)
        {
            gamePointsText.text = gamePoints.ToString();
        }

        if (earnedPointsText != null)
        {
            earnedPointsText.text = earnedPoints.ToString();
        }
    }

    /// <summary>
    /// Sets the total points and saves the data.
    /// </summary>
    /// <param name="points">New total points value.</param>
    public void SetGamePoints(int points)
    {
        gamePoints = points;
        SavePoints();
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Sets the earned points and saves the data.
    /// </summary>
    /// <param name="points">New earned points value.</param>
    public void SetEarnedPoints(int points)
    {
        earnedPoints = points;
        SavePoints();
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Saves the points to PlayerPrefs.
    /// </summary>
    private void SavePoints()
    {
        PlayerPrefs.SetInt(GamePointsKey, gamePoints);
        PlayerPrefs.SetInt(EarnedPointsKey, earnedPoints);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the points from PlayerPrefs.
    /// </summary>
    private void LoadPoints()
    {
        gamePoints = PlayerPrefs.GetInt(GamePointsKey, 500); // Default to 500 if no data exists
        earnedPoints = PlayerPrefs.GetInt(EarnedPointsKey, 300); // Default to 300 if no data exists
    }

    /// <summary>
    /// Clears the points data in PlayerPrefs and resets to defaults.
    /// </summary>
    public void ClearPoints()
    {
        PlayerPrefs.DeleteKey(GamePointsKey);
        PlayerPrefs.DeleteKey(EarnedPointsKey);

        gamePoints = 500; // Reset to default
        earnedPoints = 300; // Reset to default
        
        UpdatePointsDisplay();
    }

    private void Update()
    {
        Debug.Log(" credit value" + isCredit);
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Call UpdateWalletPoints method
            UpdateWalletPoints();
        }
    }

    private void UpdateWalletPoints()
    {
        var data = new UpdateWalletRequest(isCredit, gamePoints, earnedPoints);
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
public class UpdateWalletRequest
{
    public bool isCredit;
    public int gamingPoints;
    public int earnedPoints;
    public UpdateWalletRequest(bool isCredit,int gamingPoints,int earnedPoints)
    {
        this.isCredit = isCredit;
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

