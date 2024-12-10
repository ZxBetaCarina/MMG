using System;
using UnityEngine;
using TMPro; // If you're using TextMeshPro for displaying text

public class BonusPointsTimer : MonoBehaviour
{
    private const string TimerStartKey = "BonusPointsTimerStart";
    //private readonly TimeSpan timerDuration = TimeSpan.FromDays(7);
    private readonly TimeSpan timerDuration = TimeSpan.FromMinutes(8);
    public TotalPoints totalPoints; // Reference to the TotalPoints script
    public TMP_Text countdownText; // Reference to a TextMeshPro text object for countdown display

    private void Start()
    {
        // Initialize the timer if not already started
        if (!PlayerPrefs.HasKey(TimerStartKey))
        {
            StartTimer();
        }
    }

    private void Update()
    {
        // Check and print the remaining time
        TimeSpan remainingTime = GetRemainingTime();
        if (remainingTime.TotalSeconds > 0)
        {
            DisplayCountdown(remainingTime);
        }
        else
        {
            ResetBonusPoints();
        }
    }

    public void StartTimer()
    {
        // Save the current time as the start time
        PlayerPrefs.SetString(TimerStartKey, DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
    }

    private bool IsTimerExpired()
    {
        return GetRemainingTime().TotalSeconds <= 0;
    }

    private TimeSpan GetRemainingTime()
    {
        if (!PlayerPrefs.HasKey(TimerStartKey)) return TimeSpan.Zero;

        DateTime startTime = DateTime.Parse(PlayerPrefs.GetString(TimerStartKey));
        DateTime endTime = startTime + timerDuration;
        return endTime - DateTime.UtcNow;
    }

    private void ResetBonusPoints()
    {
        if (totalPoints != null)
        {
            //totalPoints.BonusPoints = 0;
            totalPoints.UpdateWalletPoints();
            Debug.Log("Bonus points have been reset to 0.");
        }

        // Restart the timer if needed
        StartTimer();
    }

    private void DisplayCountdown(TimeSpan remainingTime)
    {
        if (countdownText != null)
        {
            // Format the remaining time as days, hours, minutes, and seconds
            countdownText.text = $"{remainingTime.Days:D2}d {remainingTime.Hours:D2}h {remainingTime.Minutes:D2}m {remainingTime.Seconds:D2}s";
        }
    }
}
