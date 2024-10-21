using System;
using UnityEngine;
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

    private void Start()
    {
        GetProfile();
    }

    private void OnEnable()
    {
        notification.onClick.AddListener(OnNotificationClick);
        wallet.onClick.AddListener(OnWalletClick);
        ludo.onClick.AddListener(OnLudoClick);
        teenPatti.onClick.AddListener(OnTeenPattiClick);
        currency.onClick.AddListener(OnCurrencyClick);
        chess.onClick.AddListener(OnChessClick);
        joinBtt.onClick.AddListener(OnJoinBttClick);
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
    }

    private void OnNotificationClick()
    {
    }

    private void OnWalletClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnLudoClick()
    {
    }

    private void OnTeenPattiClick()
    {
    }

    private void OnCurrencyClick()
    {
    }

    private void OnChessClick()
    {
    }

    private void OnJoinBttClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.JoinGiveaway);
    }

    private void GetProfile()
    {
        ApiManager.Get<ProfileResponseData>(ServiceURLs.GetProfile, OnSuccessGetProfile, OnErrorGetProfile);
    }

    private void OnSuccessGetProfile(ProfileResponseData obj)
    {
        if (obj.status)
        {
            UserData.SetTotalData(obj.data);
            CustomLog.SuccessLog("UserData Updated");
        }
        else
        {
            CustomLog.ErrorLog(obj.message);
        }
    }

    private void OnErrorGetProfile(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
}