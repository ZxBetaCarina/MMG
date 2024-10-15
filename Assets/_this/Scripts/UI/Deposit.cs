using UnityEngine;
using UnityEngine.UI;

public class Deposit : MonoBehaviour
{
    [SerializeField] private Button back;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }
}