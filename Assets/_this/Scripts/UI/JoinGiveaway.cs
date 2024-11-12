using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGiveaway : MonoBehaviour
{
    [SerializeField] private Button minus;
    [SerializeField] private Button plus;
    [SerializeField] private Button join;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private int count = 0;

    private void OnEnable()
    {
        minus.onClick.AddListener(OnMinus);
        plus.onClick.AddListener(OnPlus);
        join.onClick.AddListener(OnJoin);
    }

    private void OnDisable()
    {
        minus.onClick.RemoveListener(OnMinus);
        plus.onClick.RemoveListener(OnPlus);
        join.onClick.RemoveListener(OnJoin);
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

    private void OnJoin()
    {
        if (count > 0)
        {
            UIManager.LoadScreenAnimated(UIScreen.QrCode);
        }
        else
        {
            PopUpManager.ShowPopUpAction("Message", "Must enter a quantity", AfterOk);
        }
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