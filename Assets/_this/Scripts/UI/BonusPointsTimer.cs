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
    
    public bool isTicketPurchased = false;

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
        // Subscribe to the OnProfileLoaded event to ensure we get the profile data before using it
        Profile.OnProfileLoaded += OnProfileLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe when this object is disabled to avoid memory leaks
        Profile.OnProfileLoaded -= OnProfileLoaded;
    }

    // This method is called when the profile is fully loaded
    private void OnProfileLoaded()
    {
        string createdAtString = UserData.GetData(UserDataSet.CreatedAt);
        startDate = DateTime.Parse(createdAtString); // Parse the string back to DateTime
        targetDate = startDate.AddDays(7);

        
    }

    private void Update()
    {
        Debug.Log($"Start Date: {isTicketPurchased}, Target Date: {targetDate}");
    }

    public bool IsTimeExpired()
    {
        
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
    private void OnCheckTrialTime()
    {
        ApiManager.Post<TrialTimeResponseData>(ServiceURLs.CheckTrialPeriod, OnSuccess, OnError);
    }

    // On API call success, populate ticketNumbers and create tickets
    private void OnSuccess(TrialTimeResponseData response)
    {
        targetDate = response.data.dateRedeemed;
    }

    // On API call error
    private void OnError(string errorMessage)
    {
        CustomLog.ErrorLog(errorMessage);
    }
}
public class TrialTimeResponseData
{
    public bool status { get; set; }
    public string message { get; set; }
    public TrialTimeData data { get; set; }  // Change 'TicketData' to 'data'
}

public class TrialTimeData
{
    public bool isRedeemed { get; set; }
    public DateTime dateRedeemed { get; set; }
}
