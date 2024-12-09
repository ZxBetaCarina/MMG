using UnityEngine;
using System;
public class TrailPeriod : MonoBehaviour
{
    public bool isTrailPeriod = true;
    
    [Header("start date")]
    // Serialized fields for DateTime components
    [SerializeField] private int startYear;
    [SerializeField] private int startMonth;
    [SerializeField] private int startDay;
    [SerializeField] private int startHour;
    [SerializeField] private int startMinute;
    [SerializeField] private int startSecond;

    [Header("end date")]
    [SerializeField] private int endYear;
    [SerializeField] private int endMonth;
    [SerializeField] private int endDay;
    [SerializeField] private int endHour;
    [SerializeField] private int endMinute;
    [SerializeField] private int endSecond;

    private DateTime startDate;
    private DateTime endDate;
  
public static TrailPeriod instance;

private void Awake()
{
    instance = this;
}

public void SetDate()
{
    
}
public void GetDate()
{
    
}

public void Settrailperiod()
{
    isTrailPeriod = true;
    TotalPoints.instance.setbonusPoints(20);
    StarttraialPeriod();
}
public void StarttraialPeriod()
{
    // Reconstruct the startDate and endDate using the serialized values
    startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
    endDate = startDate.AddDays(7);  // Add 7 days to startDate

    // Store the endDate components for visibility
    endYear = endDate.Year;
    endMonth = endDate.Month;
    endDay = endDate.Day;
    endHour = endDate.Hour;
    endMinute = endDate.Minute;
    endSecond = endDate.Second;

    // Log the start and end dates for reference
    Debug.Log("Start Date: " + startDate);
    Debug.Log("End Date (7 days later): " + endDate);
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            StarttraialPeriod();
            if (checktrailperiodcompleted())
            {
                CompletedTrailPeriod();
            }
        }
        // Check if the current date is the 8th day from the start date
      
    }

    public bool checktrailperiodcompleted()
    {
        if ( isTrailPeriod&& DateTime.Now.Date >= endDate.Date)
        {
            return true;
           
            // Perform actions when 7 days have passed (e.g., trigger events)
        }
        else 
        {
            Debug.Log("still In time Period");
            isTrailPeriod = true;
        }

        return false;
    }
   public void CompletedTrailPeriod()
    {
        isTrailPeriod = false;
        
        Debug.Log("Trail period completed.");
    }
}