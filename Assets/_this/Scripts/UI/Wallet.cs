using System;
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
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
        deposit.onClick.AddListener(OnDeposit);
        withdraw.onClick.AddListener(OnWithdraw);
        buyTicket.onClick.AddListener(OnBuyTicket);
        OnGetPoints();
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
        deposit.onClick.RemoveListener(OnDeposit);
        withdraw.onClick.RemoveListener(OnWithdraw);
        buyTicket.onClick.RemoveListener(OnBuyTicket);
    }

    private void Update()
    {
        gamingPoints.text = TotalPoints.instance.gamePoints.ToString();
        earnedPoints.text = TotalPoints.instance.earnedPoints.ToString();
        
    }

    private void OnGetPoints()
    {
        GetWalletApi.GetWalletAmount(OnSuccess,OnError);
    }

    private void OnSuccess((string, string) obj)
    {
        //earnedPoints.text = obj.Item1;
        //gamingPoints.text = obj.Item2;
    }

    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
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
        UIManager.LoadScreenAnimated(UIScreen.BuyTicket);
        
    }
}