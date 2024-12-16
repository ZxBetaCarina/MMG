using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class BonusPointsTimer : MonoBehaviour
{
    public static BonusPointsTimer _instance;
    
    public TMP_Text countdownText; // Reference to a TextMeshPro text object for countdown display

    private DateTime startDate; // The starting date of the timer
    private DateTime targetDate; // The target date (7 days after startDate)

    public int startYear; // Example year
    public int startMonth; // Example month
    public int startDay; // Example day

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        // Initialize the start and target dates
        startDate = new DateTime(startYear, startMonth, startDay);
        targetDate = startDate.AddDays(7);

    }

    private void Update()
    {
        print(IsTimeExpired());
    }

    public bool IsTimeExpired()
    {
        // if (myTicketsScript != null && myTicketsScript.GetTicketCount() > 0)
        // {
        //     return true;  // expire if there are tickets
        // }
        // Check if the current UTC time is past the target date
        return DateTime.UtcNow > targetDate;
        
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
