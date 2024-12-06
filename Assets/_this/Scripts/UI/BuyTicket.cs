using TMPro;
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
        UIManager._onbackbuttonpressed += OnBack;
        minus.onClick.AddListener(OnMinus);
        plus.onClick.AddListener(OnPlus);
        Buy.onClick.AddListener(OnBuy);
        Back.onClick.AddListener(OnBack);
        UpdateQuantityText();
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        minus.onClick.RemoveListener(OnMinus);
        plus.onClick.RemoveListener(OnPlus);
        Buy.onClick.RemoveListener(OnBuy);
        Back.onClick.RemoveListener(OnBack);
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
            ApiManager.Post<SendResponseData>(ServiceURLs.SendRequest, OnSuccess, OnError);
        }
        else
        {
            PopUpManager.ShowPopUp("Message", "Must enter a quantity");
        }
    }

    private void OnSuccess(SendResponseData obj)
    {
        if (obj.status)
        {
            PopUpManager.ShowPopUp("Request sent successfully",
                " Thank you for requesting a treasure ticket. Our sales executive will contact you shortly. \n\n Thanks for your cooperation.");
        }
    }
    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    private void OnBack()
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