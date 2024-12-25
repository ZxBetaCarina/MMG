using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTransactions : MonoBehaviour
{
    // Singleton instance
    public static UpdateTransactions Instance;

    // Variables to store titles and amounts
    private string earnedTitle;
    private int earnedAmount;
    
    private string gameTitle;
    private int gameAmount;

    // Start is called before the first frame update
    void Start()
    {
        // Check if there's already an instance, if so, destroy this one (enforcing Singleton)
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  // Optional: keeps the instance across scenes
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: You can put any continuous checks or operations here
    }

    // Function to update earned transactions
    public void UpdateEarnedTransactions(string title, int amount)
    {
        earnedTitle = title;
        earnedAmount = amount;

        // You can add additional logic to update UI or save data, if needed
        //Debug.Log("Earned Transactions Updated: " + earnedTitle + " - " + earnedAmount);
    }

    // Function to update game transactions
    public void UpdateGameTransactions(string title, int amount)
    {
        gameTitle = title;
        gameAmount = amount;

        // You can add additional logic to update UI or save data, if needed
        //Debug.Log("Game Transactions Updated: " + gameTitle + "  " + gameAmount);
    }
}