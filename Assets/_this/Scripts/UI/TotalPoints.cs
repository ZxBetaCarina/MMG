using TMPro;
using UnityEngine;

public class TotalPoints : MonoBehaviour
{
    public static TotalPoints Instance { get; private set; }  // Singleton instance
    public float totalPoints = 500; // Default value

    public TextMeshProUGUI pointsText;

    private void Awake()
    {
        // Ensure only one instance of TotalPoints exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Load totalPoints from PlayerPrefs when the scene starts
            LoadTotalPoints();
        }
    }

    void Start()
    {
        UpdatePointsDisplay();
    }

    void Update()
    {
        UpdatePointsDisplay();
    }

    // Method to update points display
    void UpdatePointsDisplay()
    {
        pointsText.text = totalPoints.ToString();
    }

    // Method to set the totalPoints and save it
    public void SetTotalPoints(float points)
    {
        totalPoints = points;
        UpdatePointsDisplay();
        SaveTotalPoints();
    }

    // Method to save totalPoints to PlayerPrefs
    private void SaveTotalPoints()
    {
        PlayerPrefs.SetFloat("TotalPoints", totalPoints);
        PlayerPrefs.Save();  // Save the data to disk
    }

    // Method to load totalPoints from PlayerPrefs
    private void LoadTotalPoints()
    {
        if (PlayerPrefs.HasKey("TotalPoints"))
        {
            totalPoints = PlayerPrefs.GetFloat("TotalPoints");
        }
        else
        {
            totalPoints = 500; // Default value if no data is found
        }
    }

    // Optionally, clear the PlayerPrefs (e.g., when starting a new game)
    public void ClearTotalPoints()
    {
        PlayerPrefs.DeleteKey("TotalPoints");
        totalPoints = 500; // Reset to default
        UpdatePointsDisplay();
    }
}