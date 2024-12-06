using UnityEngine;
using System;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    private int year = 2025;
    private int month = 1;
    private int day = 1;
    private int hour = 0;
    private int minute = 0;
    private int second = 0;

    private DateTime targetTime;
    private TimeSpan remainingTime;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        
        try
        {
            targetTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            UpdateRemainingTime();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error setting target time: " + ex.Message);
        }
    }

    void Update()
    {
        UpdateRemainingTime();
        countdownText.text = remainingTime.ToString(@"dd\:hh\:mm\:ss");
        if (remainingTime.TotalSeconds <= 0)
        {
        }
    }

    void UpdateRemainingTime()
    {
        DateTime currentUtcTime = DateTime.UtcNow;
        remainingTime = targetTime - currentUtcTime;
        if (remainingTime.TotalSeconds < 0)
        {
            remainingTime = TimeSpan.Zero;
        }
    }
    public void GetTargetTime()
    {
        ApiManager.Get<GiveawayTimerResponse>(ServiceURLs.GetGiveawayTimer, OnSuccessGetTime, OnErrorGetWalletGetTime);
    }

    private void OnSuccessGetTime(GiveawayTimerResponse obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.status + obj.message);
            Debug.Log(obj.data.giveawayDate.year);
            Debug.Log(obj.data.giveawayDate.month);
            Debug.Log(obj.data.giveawayDate.day);

            // Assign year, month, day from the API response
            year = obj.data.giveawayDate.year;
            month = obj.data.giveawayDate.month;
            day = obj.data.giveawayDate.day;

            // Set hour, minute, and second to 0
            hour = 0;
            minute = 0;
            second = 0;

        }
    }

    private void OnErrorGetWalletGetTime(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
}
public class GiveawayTimerResponse
{
    public bool status;
    public string message;
    public GiveawayTimerData data;
}

public class GiveawayTimerData
{
    public string _id { get; set; }
    public string title { get; set; }
    public GiveawayDate giveawayDate { get; set; }
    public bool isActive { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class GiveawayDate
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
}