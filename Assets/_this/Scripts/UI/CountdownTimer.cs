using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class CountdownTimer : MonoBehaviour
{
    // Store the date components in a list
    [Header("year, month, day, hour, minute, second")]
    public List<int> endDate = new List<int> { 2024, 12, 31, 0, 0, 0 };  // {year, month, day, hour, minute, second}

    // Store the target UTC DateTime
    private DateTime targetTime;

    // To display the remaining time in a readable format
    private TimeSpan remainingTime;

    // Reference to the TextMeshProUGUI UI element for countdown display
    public TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the list has exactly 6 items (year, month, day, hour, minute, second)
        if (endDate.Count == 6)
        {
            // Construct the DateTime object using the values from the list
            targetTime = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], endDate[5], DateTimeKind.Utc);
            UpdateRemainingTime();
        }
        else
        {
            Debug.LogError("End Date list must contain exactly 6 items (year, month, day, hour, minute, second).");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update remaining time every frame
        UpdateRemainingTime();

        // Display the countdown in the TextMeshPro UI element
        countdownText.text = remainingTime.ToString(@"dd\:hh\:mm\:ss");

        // Example of triggering an event when the countdown reaches zero
        if (remainingTime.TotalSeconds <= 0)
        {
            countdownText.text = "Time's up!";  // Display a message when countdown finishes
            Debug.Log("Countdown finished!");
        }
    }

    // Method to calculate the remaining time
    void UpdateRemainingTime()
    {
        // Get the current UTC time
        DateTime currentUtcTime = DateTime.UtcNow;

        // Calculate the remaining time
        remainingTime = targetTime - currentUtcTime;

        // Ensure the remaining time doesn't go negative (in case we went past the target)
        if (remainingTime.TotalSeconds < 0)
        {
            remainingTime = TimeSpan.Zero;
        }
    }
}