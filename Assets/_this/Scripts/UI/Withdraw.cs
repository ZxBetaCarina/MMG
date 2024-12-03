using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Withdraw : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private TMP_InputField getCoins;
    [SerializeField] private TMP_Text earnedPoints;
    [SerializeField] private Button submit;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        submit.onClick.AddListener(OnSubmit);
        OnGetPoints();
    }

    private void OnDisable()
    {
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
        // Check if the input value is valid
        if (string.IsNullOrEmpty(getCoins.text) || !int.TryParse(getCoins.text, out int requestedAmount))
        {
            earnedPoints.text = "Please enter a valid amount.";
            return;
        }

        // Get the total points from the TotalPoints singleton
        float totalPoints = TotalPoints.Instance.totalPoints;

        // Check if the requested amount is less than or equal to the available points
        if (requestedAmount > totalPoints)
        {
            earnedPoints.text = "Insufficient balance.";
        }
        else
        {
            // Successful withdrawal, subtract the requested amount and update totalPoints
            TotalPoints.Instance.SetTotalPoints(totalPoints - requestedAmount);

            // Update the UI
            earnedPoints.text = $"Withdrawal successful! New balance: {TotalPoints.Instance.totalPoints}";
        }
    }
}