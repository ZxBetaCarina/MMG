using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MyTickets : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private GameObject ticketPrefab;    // The ticket prefab
    [SerializeField] private Transform ticketParent;     // The parent where tickets will be placed
    
    // Expose the list of ticket numbers to the Unity Inspector
    private List<string> ticketNumbers = new List<string>();
    
    // List to keep track of instantiated tickets for cleanup
    private List<GameObject> instantiatedTickets = new List<GameObject>();

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
        
        // Call the API to get ticket data when the script is enabled
        OnReqTicket();
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
        
        // Clear ticket data and destroy instantiated ticket GameObjects
        ClearTickets();
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    // Create tickets based on the ticket numbers
    private void CreateTickets()
    {
        // Ensure the list is not empty
        if (ticketNumbers.Count > 0)
        {
            // Loop through each ticket number in the list
            for (int i = 0; i < ticketNumbers.Count; i++)
            {
                // Instantiate a new ticket prefab
                GameObject newTicket = Instantiate(ticketPrefab, ticketParent);
                instantiatedTickets.Add(newTicket); // Add the new ticket to the list for future cleanup

                // Get the Ticket script from the new prefab instance
                Ticket ticketScript = newTicket.GetComponent<Ticket>();
                if (ticketScript != null)
                {
                    // Set the ticket number for the prefab (based on the list)
                    ticketScript.SetTicketNumber(ticketNumbers[i]);
                }
                else
                {
                    Debug.LogWarning("Ticket prefab does not have a Ticket script attached.");
                }
            }
        }
        else
        {
            Debug.LogError("Ticket list is empty. Please provide ticket numbers in the inspector.");
        }
    }
    // API request to fetch ticket data
    private void OnReqTicket()
    {
        ApiManager.Post<TicketResponseData>(ServiceURLs.MyTickets, OnSuccess, OnError);
    }

    // On API call success, populate ticketNumbers and create tickets
    private void OnSuccess(TicketResponseData response)
    {
        ClearTickets();
        // Check if the response contains valid data
        if (response != null && response.data != null)
        {
            print("Number of tickets: " + response.data.Count);

            // Populate ticketNumbers with the ticketIds from the response
            ticketNumbers.Clear();  // Clear any previous data in the list
            foreach (var ticket in response.data)
            {
                ticketNumbers.Add(ticket.ticketId);  // Add each ticketId to the ticketNumbers list
            }

            // Create the tickets based on the ticket numbers
            CreateTickets();
        }
        else
        {
            print("No ticket data received.");
        }
    }

    // On API call error
    private void OnError(string errorMessage)
    {
        print("Error fetching ticket data: " + errorMessage);
    }

    // Function to clear tickets and cleanup
    private void ClearTickets()
    {
        // Clear the list of ticket numbers
        ticketNumbers.Clear();

        // Destroy all instantiated tickets
        foreach (var ticket in instantiatedTickets)
        {
            Destroy(ticket);
        }
        // Clear the list of instantiated tickets after destruction
        instantiatedTickets.Clear();
    }
}

// API response data classes
public class TicketResponseData
{
    public bool status { get; set; }
    public string message { get; set; }
    public List<TicketData> data { get; set; }  // Change 'TicketData' to 'data'
}

public class TicketData
{
    public string _id { get; set; }
    public string gameUserId { get; set; }
    public string sellerId { get; set; }
    public string ticketId { get; set; }  // This is the ticket number you want to show
    public int ticketValue { get; set; }
    public bool isRedeemed { get; set; }
    public DateTime dateRedeemed { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}
