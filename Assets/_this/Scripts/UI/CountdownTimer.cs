using UnityEngine;
using System;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    private int year;
    private int month;
    private int day;
    private int hour;
    private int minute;
    private int second;

    private bool gottime = false;

    private DateTime targetTime;
    private TimeSpan remainingTime;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        
        if (gottime == true)
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
        
    }

    void Update()
    {
        if (gottime == true)
        {
            UpdateRemainingTime();
            countdownText.text = remainingTime.ToString(@"dd\:hh\:mm\:ss");
            if (remainingTime.TotalSeconds <= 0)
            {
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetTargetTime();
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
            gottime = true;

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