using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private Button notification;
    [SerializeField] private Button wallet;
    [SerializeField] private Button ludo;
    [SerializeField] private Button teenPatti;
    [SerializeField] private Button currency;
    [SerializeField] private Button chess;
    [SerializeField] private Button BasketBall;
    [SerializeField] private Button joinBtt;
    [SerializeField] private Button timerBtn;
    
    [SerializeField] private TMP_Text PointsUI;

    private bool hasPurchasedTickets = false;
    
    [SerializeField] private Image joinBttImage; // Reference to the button's Image component
    [SerializeField] private Sprite GiveAway; // Sprite when the user has purchased tickets
    [SerializeField] private Sprite BuyTicket;

    private void OnEnable()
    {
        notification.onClick.AddListener(OnNotificationClick);
        wallet.onClick.AddListener(OnWalletClick);
        ludo.onClick.AddListener(OnLudoClick);
        teenPatti.onClick.AddListener(OnTeenPattiClick);
        currency.onClick.AddListener(OnCurrencyClick);
        chess.onClick.AddListener(OnChessClick);
        BasketBall.onClick.AddListener(OnBasketBallClick);
        joinBtt.onClick.AddListener(OnJoinBttClick);
        timerBtn.onClick.AddListener(OntimerBttClick);
        TotalPoints.instance.GetWallet();
        Profile.GetProfile();
        Profile.OnProfileLoaded += OnProfileLoaded;
    }

    private void OnDisable()
    {
        notification.onClick.RemoveListener(OnNotificationClick);
        wallet.onClick.RemoveListener(OnWalletClick);
        ludo.onClick.RemoveListener(OnLudoClick);
        teenPatti.onClick.RemoveListener(OnTeenPattiClick);
        currency.onClick.RemoveListener(OnCurrencyClick);
        chess.onClick.RemoveListener(OnChessClick);
        BasketBall.onClick.RemoveListener(OnBasketBallClick);
        joinBtt.onClick.RemoveListener(OnJoinBttClick);
        timerBtn.onClick.RemoveListener(OntimerBttClick);
        Profile.OnProfileLoaded -= OnProfileLoaded;
    }

    private void Update()
    {
        PointsUI.text = (TotalPoints.instance.gamePoints + TotalPoints.instance.BonusPoints).ToString();
    }
    private void OnProfileLoaded()
    {
        // After profile is loaded, get the value of HasPurchasedTickets
        hasPurchasedTickets = bool.Parse(UserData.GetData(UserDataSet.HasPurchasedTickets));
        if (!hasPurchasedTickets) // Only proceed if HasPurchasedTickets is true
        {
            joinBttImage.sprite = GiveAway;
        }
        else
        {
            joinBttImage.sprite = BuyTicket;
        }
        
    }

    private void OnNotificationClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.Notifications);
    }

    private void OnWalletClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnLudoClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.LudoBetUI);
    }

    private void OnTeenPattiClick()
    {
    }

    private void OnCurrencyClick()
    {
        PlayerPrefs.SetInt("InGame", 1);
        SceneManager.LoadScene(1);
    }

    private void OnChessClick()
    {
    }
    private void OnBasketBallClick()
    {
        SceneManager.LoadScene(3);
    }

    private void OnJoinBttClick()
    {
        if (!hasPurchasedTickets) // Only proceed if HasPurchasedTickets is true
        {
            UIManager.LoadScreenAnimated(UIScreen.JoinGiveaway);
            joinBttImage.sprite = GiveAway;
        }
        else
        {
            UIManager.LoadScreenAnimated(UIScreen.BuyTicket);
            joinBttImage.sprite = BuyTicket;
        }
    }
    private void OntimerBttClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.MyTickets);
    }
    
}