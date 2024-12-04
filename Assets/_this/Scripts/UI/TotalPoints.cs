using TMPro;
using UnityEngine;

public class TotalPoints : MonoBehaviour
{
    public static TotalPoints instance;
    
    [Header("Points Data")]
    public float gamePoints = 500; // Default points value
    public float earnedPoints = 300; // Default earned points

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI gamePointsText;
    [SerializeField] private TextMeshProUGUI earnedPointsText;

    private const string GamePointsKey = "GamePoints"; // Key for PlayerPrefs
    private const string EarnedPointsKey = "EarnedPoints"; // Key for PlayerPrefs

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load saved points when the scene starts
        LoadPoints();
    }

    private void Start()
    {
        // Initial update for UI
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Updates the points displayed in the UI.
    /// </summary>
    private void UpdatePointsDisplay()
    {
        if (gamePointsText != null)
        {
            gamePointsText.text = gamePoints.ToString();
        }

        if (earnedPointsText != null)
        {
            earnedPointsText.text = earnedPoints.ToString();
        }
    }

    /// <summary>
    /// Sets the total points and saves the data.
    /// </summary>
    /// <param name="points">New total points value.</param>
    public void SetGamePoints(float points)
    {
        gamePoints = points;
        SavePoints();
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Sets the earned points and saves the data.
    /// </summary>
    /// <param name="points">New earned points value.</param>
    public void SetEarnedPoints(float points)
    {
        earnedPoints = points;
        SavePoints();
        UpdatePointsDisplay();
    }

    /// <summary>
    /// Saves the points to PlayerPrefs.
    /// </summary>
    private void SavePoints()
    {
        PlayerPrefs.SetFloat(GamePointsKey, gamePoints);
        PlayerPrefs.SetFloat(EarnedPointsKey, earnedPoints);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the points from PlayerPrefs.
    /// </summary>
    private void LoadPoints()
    {
        gamePoints = PlayerPrefs.GetFloat(GamePointsKey, 500); // Default to 500 if no data exists
        earnedPoints = PlayerPrefs.GetFloat(EarnedPointsKey, 300); // Default to 300 if no data exists
    }

    /// <summary>
    /// Clears the points data in PlayerPrefs and resets to defaults.
    /// </summary>
    public void ClearPoints()
    {
        PlayerPrefs.DeleteKey(GamePointsKey);
        PlayerPrefs.DeleteKey(EarnedPointsKey);

        gamePoints = 500; // Reset to default
        earnedPoints = 300; // Reset to default
        
        UpdatePointsDisplay();
    }
}
