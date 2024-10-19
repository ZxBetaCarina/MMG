﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private Button deposit;
    [SerializeField] private Button withdraw;
    [SerializeField] private Button buyTicket;
    [SerializeField] private TMP_Text gamingPoints;
    [SerializeField] private TMP_Text earnedPoints;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        deposit.onClick.AddListener(OnDeposit);
        withdraw.onClick.AddListener(OnWithdraw);
        buyTicket.onClick.AddListener(OnBuyTicket);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        deposit.onClick.RemoveListener(OnDeposit);
        withdraw.onClick.RemoveListener(OnWithdraw);
        buyTicket.onClick.RemoveListener(OnBuyTicket);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnDeposit()
    {
        UIManager.LoadScreenAnimated(UIScreen.Deposit);
    }

    private void OnWithdraw()
    {
        UIManager.LoadScreenAnimated(UIScreen.Withdraw);
    }

    private void OnBuyTicket()
    {
        UIManager.LoadScreenAnimated(UIScreen.ExchangeCoins);
    }
}