﻿using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameConfigrationController : MonoBehaviour
{
    public GameObject TitleText;
    public GameObject bidText;
    public GameObject MinusButton;
    public GameObject PlusButton;
    public GameObject[] Toggles;
    private int currentBidIndex = 0;
    
   // public GameObject Canclebutton2;

    private MyGameMode[] modes = new MyGameMode[] { MyGameMode.Classic, MyGameMode.Quick, MyGameMode.Master };

    public GameObject privateRoomJoin;
    private SetMyData smd; 

    public static GameConfigrationController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        smd = GetComponent<SetMyData>();
    }


    // Update is called once per frame
    void Update()
    {
    }


    void OnEnable()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            int index = i;
            Toggles[i].GetComponent<Toggle>().onValueChanged.AddListener(
                (value) => { ChangeGameMode(value, modes[index]); }
            );
        }

        currentBidIndex = 0;
        UpdateBid(true);

        Toggles[0].GetComponent<Toggle>().isOn = true;
        GameManager.Instance.mode = MyGameMode.Classic;

        switch (GameManager.Instance.type)
        {
            case MyGameType.TwoPlayer:
                TitleText.GetComponent<Text>().text = "Two Players";
                break;
            case MyGameType.FourPlayer:
                TitleText.GetComponent<Text>().text = "Four Players";
                break;
            case MyGameType.Private:
                TitleText.GetComponent<Text>().text = "Private Room";
                privateRoomJoin.SetActive(true);
                break;
        }
    }

    public void turnonButtons()
    {
    }

    void OnDisable()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            int index = i;
            Toggles[i].GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
        }

        privateRoomJoin.SetActive(false);
        currentBidIndex = 0;
        UpdateBid(false);
        Toggles[0].GetComponent<Toggle>().isOn = true;
        Toggles[1].GetComponent<Toggle>().isOn = false;
        Toggles[2].GetComponent<Toggle>().isOn = false;
    }

    public void setCreatedProvateRoom()
    {
        GameManager.Instance.JoinedByID = false;
    }

  //  public GameObject CantStartGamePopup;
    public InitMenuScript _initMenuScript;
    public GameObject cancelBtn;

    
    public void PressedStartGame1v1WithBots()
    {
        //if (PlayFabManager._instance.isInLobby && PlayFabManager._instance.isInMaster)
        if(PhotonNetwork.insideLobby&& PhotonNetwork.connectedAndReady )
        {
            
            // Show the game configuration and set up the room
            _initMenuScript.ShowGameConfiguration(0);
            // Call to create the private room and check cancellation
            Invoke("setCreatedProvateRoom",0f);
            // Call to start the game
            Invoke("startGame",0f);
        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Waiting For Server Connection, Please Wait");
        }
    }
    public void CancelCreatedGame()
    {
        // if (StaticStrings.showAdWhenLeaveGame)
        //     AdsManager.Instance.adsScript.ShowAd();
        PlayerPrefs.SetInt("InGame", 1);
        PhotonNetwork.BackgroundTimeout = StaticStrings.photonDisconnectTimeoutLong; ;
        Debug.Log("Timeout 3");
        //GameManager.Instance.cueController.removeOnEventCall();
        PhotonNetwork.LeaveRoom();

        GameManager.Instance.playfabManager.roomOwner = false;
        GameManager.Instance.roomOwner = false;
        GameManager.Instance.resetAllData();
        SceneManager.LoadScene("Game");
        //   GameManager.Instance.playfabManager.splashCanvas.SetActive(false);
       

    }
// Cancel button press logic to set the flag
    // public void OnCancelButtonPressed()
    // {
    //     // Set the flag to true when cancel button is pressed
    //     cancellationRequested = true;
    //
    //     // Optionally, hide or update the UI to reflect the cancellation
    //     smd.hidePalyerMatch.gameObject.SetActive(false);
    //     CancelInvoke("setCreatedProvateRoom");
    //     CancelInvoke("StartGame");
    //     
    //
    //     // Optionally, show a pop-up message for cancellation
    //     PopUpManager.ShowPopUp("Cancelled", "Game Start has been cancelled.");
    // }
    public void PressedStartGame1v1()
    {
       // if (PlayFabManager._instance.isInLobby && PlayFabManager._instance.isInMaster)
       if(PhotonNetwork.insideLobby&& PhotonNetwork.connectedAndReady )
        {
            _initMenuScript.ShowGameConfiguration(0);
            setCreatedProvateRoom();
            startGame();
            cancelBtn.SetActive(true);
        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Waiting For Server Connection, Please Wait");
        }
    }

    public void pressedstartGamePrivate()
    {
        
       // Canclebutton2.gameObject.SetActive(false); 
       // if (PlayFabManager._instance.isInLobby && PlayFabManager._instance.isInMaster)
       if(PhotonNetwork.insideLobby&& PhotonNetwork.connectedAndReady )
       {
          // GameManager.Instance.requiredPlayers = 4;
            _initMenuScript.ShowGameConfiguration(2);
        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Waiting For Server Connection, Please Wait");
        }
    }

    public void startGame()
    {
        // if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
        //{


        if (GameManager.Instance.type != MyGameType.Private)
        {
           
            if (GameManager.Instance.type == MyGameType.TwoPlayer)
            {
                print("2 player calling ");
            }
            else if (GameManager.Instance.type == MyGameType.FourPlayer)
            {
                print("4 player calling ");
            }


            GameManager.Instance.facebookManager.startRandomGame();
        }
        else
        {
            if (GameManager.Instance.JoinedByID)
            {
                Debug.Log("Joined by id!");
                GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
            }
            else
            {
                Debug.Log("Joined and created");
                GameManager.Instance.playfabManager.CreatePrivateRoom();
                GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
            }
        }
        //    }
        /*else
        {
            GameManager.Instance.dialog.SetActive(true);
        }*/
    }

    private void ChangeGameMode(bool isActive, MyGameMode mode)
    {
        if (isActive)
        {
            GameManager.Instance.mode = mode;
        }
    }


    public void IncreaseBid()
    {
        if (currentBidIndex < StaticStrings.bidValues.Length - 1)
        {
            currentBidIndex++;
            UpdateBid(true);
        }
    }

    public void DecreaseBid()
    {
        if (currentBidIndex > 0)
        {
            currentBidIndex--;
            UpdateBid(true);
        }
    }

    private void UpdateBid(bool changeBidInGM)
    {
        bidText.GetComponent<Text>().text = StaticStrings.bidValuesStrings[currentBidIndex];

        if (changeBidInGM)
            GameManager.Instance.payoutCoins = StaticStrings.bidValues[currentBidIndex];

        if (currentBidIndex == 0) MinusButton.GetComponent<Button>().interactable = false;
        else MinusButton.GetComponent<Button>().interactable = true;

        if (currentBidIndex == StaticStrings.bidValues.Length - 1)
            PlusButton.GetComponent<Button>().interactable = false;
        else PlusButton.GetComponent<Button>().interactable = true;
    }

    public void HideThisScreen()
    {
        gameObject.SetActive(false);
    }

    public bool StayinLobby = false;

    public void StopSerchingforRoom()
    {
        HideThisScreen();
        FindObjectOfType<PlayFabManager>().cancelSerching();
    }
}