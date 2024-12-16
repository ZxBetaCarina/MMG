using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ticketNumberText;    // Reference to the Text component on the prefab

    // Method to set the ticket number on the ticket prefab
    public void SetTicketNumber(string ticketNumber)
    {
        if (ticketNumberText != null)
        {
            ticketNumberText.text = ticketNumber;  // Directly set the ticket number from the list
        }
        else
        {
            Debug.LogWarning("No Text component found to display the ticket number.");
        }
    }
}