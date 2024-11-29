﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyTicket : MonoBehaviour
{
    [SerializeField] private Button minus;
    [SerializeField] private Button plus;
    [SerializeField] private Button Buy;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private int count = 1;
    [SerializeField] private Button Back;

    private void OnEnable()
    {
        minus.onClick.AddListener(OnMinus);
        plus.onClick.AddListener(OnPlus);
        Buy.onClick.AddListener(OnBuy);
        Back.onClick.AddListener(onback);
        UpdateQuantityText();
    }

    private void OnDisable()
    {
        minus.onClick.RemoveListener(OnMinus);
        plus.onClick.RemoveListener(OnPlus);
        Buy.onClick.RemoveListener(OnBuy);
        Back.onClick.RemoveListener(onback);
    }

    private void OnMinus()
    {
        if (count > 0)
        {
            count--;
            UpdateQuantityText();
        }
    }

    private void OnPlus()
    {
        count++;
        UpdateQuantityText();
    }

    private void OnBuy()
    {
        if (count > 0)
        {
            PopUpManager.ShowPopUp("Message", "Your purchase was successful");
        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Must enter a quantity");
        }
    }

    private void onback()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void AfterOk()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void UpdateQuantityText()
    {
        quantity.text = count.ToString();
    }
}