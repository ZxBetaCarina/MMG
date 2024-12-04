using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TotalPoints : MonoBehaviour
{
    public static TotalPoints Instance { get; private set; }  // Singleton instance
    
    public float gamePoints = 500; // Default value
    public TextMeshProUGUI gamePointsText;
    
    public float earnedPoints = 300; // Default value
    public TextMeshProUGUI earnedPointsText;


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
        gamePointsText.text = gamePoints.ToString();
        earnedPointsText.text = earnedPoints.ToString();
    }

    // Method to set the totalPoints and save it
    public void SetTotalPoints(float points)
    {
        gamePoints = points;
        UpdatePointsDisplay();
        SaveTotalPoints();
    }

    // Method to save totalPoints to PlayerPrefs
    private void SaveTotalPoints()
    {
        PlayerPrefs.SetFloat("GamePoints", gamePoints);
        PlayerPrefs.SetFloat("EarnedPoints", earnedPoints);
        PlayerPrefs.Save();  // Save the data to disk
    }

    // Method to load totalPoints from PlayerPrefs
    private void LoadTotalPoints()
    {
        if (PlayerPrefs.HasKey("GamePoints"))
        {
            gamePoints = PlayerPrefs.GetFloat("GamePoints");
        }
        else
        {
            gamePoints = 500; // Default value if no data is found
        }
        if (PlayerPrefs.HasKey("EarnedPoints"))
        {
            earnedPoints = PlayerPrefs.GetFloat("EarnedPoints");
        }
        else
        {
            earnedPoints = 500; // Default value if no data is found
        }
    }

    // Optionally, clear the PlayerPrefs (e.g., when starting a new game)
    public void ClearTotalPoints()
    {
        PlayerPrefs.DeleteKey("GamePoints");
        gamePoints = 500; // Reset to default
        PlayerPrefs.DeleteKey("EarnedPoints");
        earnedPoints = 500; // Reset to default
        UpdatePointsDisplay();
    }
}