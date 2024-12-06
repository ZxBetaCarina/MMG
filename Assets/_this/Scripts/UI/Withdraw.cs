﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Withdraw : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private TMP_InputField getCoins;
    [SerializeField] private TMP_Text earnedPoints;
    [SerializeField] private Button submit;

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
        submit.onClick.AddListener(OnSubmit);
        OnGetPoints();
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
        submit.onClick.RemoveListener(OnSubmit);
    }

    private void OnGetPoints()
    {
        GetWalletApi.GetWalletAmount(OnSuccess, OnError);
    }

    private void OnSuccess((string, string) obj)
    {
        earnedPoints.text = $"You Have {obj.Item1} Earned Points";
    }

    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnSubmit()
    {
        if (getCoins.text != String.Empty)
        {
           
                PopUpManager.ShowPopUp("Message", "you cant withdraw at the moment");
          //  }

        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Please Enter Amount to withdraw");
        }
    }
}
