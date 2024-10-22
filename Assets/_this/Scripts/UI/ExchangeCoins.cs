using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeCoins : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private TMP_InputField getCoins;
    [SerializeField] private TMP_Text earnedPoints;
    [SerializeField] private Button submit;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        submit.onClick.AddListener(OnSubmit);
        GetWalletApi.GetPoints += OnGetPoints;
    }
    
    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        submit.onClick.RemoveListener(OnSubmit);
        GetWalletApi.GetPoints -= OnGetPoints;
    }

    private void OnGetPoints(int earnedPoints, int gamingPoints)
    {
        this.earnedPoints.text = $"You Have {earnedPoints} Earned Points";
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnSubmit()
    {
    }
}