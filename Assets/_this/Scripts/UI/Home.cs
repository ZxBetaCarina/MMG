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
    [SerializeField] private Button joinBtt;
    [SerializeField] private Button timerBtn;
    
    [SerializeField] private TMP_Text PointsUI;

    private void OnEnable()
    {
        notification.onClick.AddListener(OnNotificationClick);
        wallet.onClick.AddListener(OnWalletClick);
        ludo.onClick.AddListener(OnLudoClick);
        teenPatti.onClick.AddListener(OnTeenPattiClick);
        currency.onClick.AddListener(OnCurrencyClick);
        chess.onClick.AddListener(OnChessClick);
        joinBtt.onClick.AddListener(OnJoinBttClick);
        timerBtn.onClick.AddListener(OntimerBttClick);
        TotalPoints.instance.GetWallet();
        Profile.GetProfile();
    }

    private void OnDisable()
    {
        notification.onClick.RemoveListener(OnNotificationClick);
        wallet.onClick.RemoveListener(OnWalletClick);
        ludo.onClick.RemoveListener(OnLudoClick);
        teenPatti.onClick.RemoveListener(OnTeenPattiClick);
        currency.onClick.RemoveListener(OnCurrencyClick);
        chess.onClick.RemoveListener(OnChessClick);
        joinBtt.onClick.RemoveListener(OnJoinBttClick);
        timerBtn.onClick.RemoveListener(OntimerBttClick);
    }

    private void Update()
    {
        PointsUI.text = (TotalPoints.instance.gamePoints + TotalPoints.instance.BonusPoints).ToString();
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

    private void OnJoinBttClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.JoinGiveaway);
    }
    private void OntimerBttClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.MyTickets);
    }
    
}