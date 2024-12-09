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

    private DateTime targetTime;
    private TimeSpan remainingTime;

    // Separate TextMeshProUGUI components for each part of the countdown
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI hoursText;
    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsText;

    private void OnEnable()
    {
        GetTargetTime();
    }

    void Update()
    {
        UpdateRemainingTime();

        // Update each TextMeshProUGUI component with the respective time value
        daysText.text = remainingTime.Days.ToString("D2"); // Two digits for consistency
        hoursText.text = remainingTime.Hours.ToString("D2");
        minutesText.text = remainingTime.Minutes.ToString("D2");
        secondsText.text = remainingTime.Seconds.ToString("D2");

        if (remainingTime.TotalSeconds <= 0)
        {
            // Logic when countdown reaches zero
        }
    }

    void UpdateRemainingTime()
    {
        // Get the current UTC time
        DateTime currentUtcTime = DateTime.UtcNow;

        // Calculate remaining time in IST (targetTime is already in IST)
        remainingTime = targetTime - currentUtcTime.AddHours(5).AddMinutes(30);

        if (remainingTime.TotalSeconds < 0)
        {
            remainingTime = TimeSpan.Zero;
        }
    }

    private void GetTargetTime()
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

            try
            {
                // Create target time in UTC and convert to IST
                DateTime targetUtcTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
                targetTime = targetUtcTime.AddHours(5).AddMinutes(30); // Convert to IST
            }
            catch (Exception ex)
            {
                Debug.LogError("Error setting target time: " + ex.Message);
            }
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
