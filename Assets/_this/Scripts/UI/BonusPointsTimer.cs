using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class BonusPointsTimer : MonoBehaviour
{
    public static BonusPointsTimer _instance;
    public TotalPoints totalPoints; // Reference to the TotalPoints script
    public TMP_Text countdownText; // Reference to a TextMeshPro text object for countdown display

    private DateTime startDate; // The starting date of the timer
    private DateTime targetDate; // The target date (7 days after startDate)

    public int startYear = 2024; // Example year
    public int startMonth = 11; // Example month
    public int startDay = 8; // Example day

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

        // Check if the current time is past the target date
        if (IsTimeExpired())
        {
            ResetBonusPoints();
        }
        else
        {
            //StartCoroutine(TimerCoroutine());
        }
    }

    private void Update()
    {
        print(IsTimeExpired());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            TimeSpan remainingTime = targetDate - DateTime.UtcNow;

            if (remainingTime.TotalSeconds > 0)
            {
                DisplayCountdown(remainingTime);
            }
            else
            {
                ResetBonusPoints();
                yield break; // Exit the coroutine once the timer ends
            }

            yield return new WaitForSeconds(1); // Update every second
        }
    }

    public bool IsTimeExpired()
    {
        // Check if the current UTC time is past the target date
        return DateTime.UtcNow > targetDate;
        
    }

    private void ResetBonusPoints()
    {
        if (totalPoints != null)
        {
            totalPoints.BonusPoints = 0;
            totalPoints.UpdateWalletPoints();
            Debug.Log("Bonus points have been reset to 0.");
        }
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
