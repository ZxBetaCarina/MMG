using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Define a custom class for earned point transaction
[System.Serializable] // To make the class visible in the Unity Inspector
public class GamePointTransaction
{
    public string description;  // The name or description of the transaction
    public float points;        // The number of points associated with the transaction

    // Constructor to initialize the description and points
    public GamePointTransaction(string description, float points)
    {
        this.description = description;
        this.points = points;
    }
}

// Define a class to hold transactions along with a date
[System.Serializable]
public class Transactionwithdate
{
    public string date;  // The date associated with the list of transactions
    public List<EarnedPointTransaction> transactions; // List of transactions on that date

    // Constructor to initialize the date and list of transactions
    public Transactionwithdate(string date, List<EarnedPointTransaction> transactions)
    {
        this.date = date;
        this.transactions = transactions;
    }
}
public class GamePoint_Transactions : MonoBehaviour
{
    // Define a list of TransactionWithDate objects and make it visible in the Unity editor
    [SerializeField] // This will allow editing the list in the Unity Editor
    private List<TransactionWithDate> transactionsByDate = new List<TransactionWithDate>();

    // Prefab for individual EarnedPointTransaction
    public GameObject transactionPrefab;
    [SerializeField] private Transform PrefabParent;

    // Prefab for the Date
    public GameObject datePrefab;

    void OnEnable()
    {
        SpawnTransactionPrefabs();
    }

    // Function to spawn the transaction prefabs based on the list
    void SpawnTransactionPrefabs()
    {
        // Iterate through each TransactionWithDate object in the list
        foreach (TransactionWithDate transactionWithDate in transactionsByDate)
        {
            // Spawn the Date prefab and set it as a child of PrefabParent
            GameObject dateInstance = Instantiate(datePrefab, PrefabParent);

            // Find the TextMeshPro component inside the Date prefab and set the date value
            TMP_Text dateText = dateInstance.GetComponentInChildren<TMP_Text>();
            if (dateText != null)
            {
                dateText.text = transactionWithDate.date;  // Set the date to the dateText
            }

            // Iterate through each transaction on this particular date
            foreach (EarnedPointTransaction transaction in transactionWithDate.transactions)
            {
                // Instantiate the transaction prefab and set it as a child of PrefabParent
                GameObject transactionInstance = Instantiate(transactionPrefab, PrefabParent);

                // Find the TextMeshPro components inside the instantiated prefab
                TMP_Text titleText = transactionInstance.transform.Find("Title").GetComponent<TMP_Text>();
                TMP_Text amountText = transactionInstance.transform.Find("Amount").GetComponent<TMP_Text>();

                // Check if the TextMeshPro components were found and assign the values
                if (titleText != null)
                {
                    titleText.text = transaction.description;  // Set the description to the title
                }

                if (amountText != null)
                {
                    amountText.text = $"{transaction.points}";  // Set the points to the amount

                    // Change the color based on whether points are positive or negative
                    if (transaction.points < 0)
                    {
                        amountText.color = Color.red;  // Set the color to red if points are negative
                    }
                    else if (transaction.points > 0)
                    {
                        amountText.color = Color.green;  // Set the color to green if points are positive
                    }
                    else
                    {
                        amountText.color = Color.white;  // Default color if points are zero
                    }
                }
            }
        }
    }
}
