using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGiveaway : MonoBehaviour
{
    [SerializeField] private Button minus;
    [SerializeField] private Button plus;
    [SerializeField] private Button join;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private int count = 1;
    [SerializeField] private Button Back;

    private void OnEnable()
    {
        minus.onClick.AddListener(OnMinus);
        plus.onClick.AddListener(OnPlus);
        join.onClick.AddListener(OnJoin);
        Back.onClick.AddListener(onback);
        UpdateQuantityText();
    }

    private void OnDisable()
    {
        minus.onClick.RemoveListener(OnMinus);
        plus.onClick.RemoveListener(OnPlus);
        join.onClick.RemoveListener(OnJoin);
        Back.onClick.RemoveListener(onback);
    }

    private void OnMinus()
    {
        if (count > 1)
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

    private void OnJoin()
    {
        if (count > 0)
        {
            UIManager.LoadScreenAnimated(UIScreen.QrCode);
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