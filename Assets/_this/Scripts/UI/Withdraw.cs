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
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        submit.onClick.RemoveListener(OnSubmit);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnSubmit()
    {
    }
}