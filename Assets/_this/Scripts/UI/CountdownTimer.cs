using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class CountdownTimer : MonoBehaviour
{
    public List<int> endDate = new List<int> { 2024, 12, 31, 0, 0, 0 };
    private DateTime targetTime;
    private TimeSpan remainingTime;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        if (endDate.Count == 6)
        {
            targetTime = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], endDate[5], DateTimeKind.Utc);
            UpdateRemainingTime();
        }
        else
        {
            Debug.LogError("End Date list must contain exactly 6 items (year, month, day, hour, minute, second).");
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
}