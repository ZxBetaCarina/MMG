using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private Button notification;
    [SerializeField] private Button wallet;
    [SerializeField] private Button ludo;
    [SerializeField] private Button teenPatti;
    [SerializeField] private Button currency;
    [SerializeField] private Button chess;
    [SerializeField] private Button joinBtt;

    private void OnEnable()
    {
        notification.onClick.AddListener(OnNotificationClick);
        wallet.onClick.AddListener(OnWalletClick);
        ludo.onClick.AddListener(OnLudoClick);
        teenPatti.onClick.AddListener(OnTeenPattiClick);
        currency.onClick.AddListener(OnCurrencyClick);
        chess.onClick.AddListener(OnChessClick);
        joinBtt.onClick.AddListener(OnJoinBttClick);
    }

    private void OnDisable()
    {
        notification.onClick.RemoveListener(OnNotificationClick);
        wallet.onClick.RemoveListener(OnWalletClick);
        ludo.onClick.RemoveListener(OnLudoClick);
        teenPatti.onClick.RemoveListener(OnTeenPattiClick);
        currency.onClick.RemoveListener(OnCurrencyClick);
        chess.onClick.RemoveListener(OnChessClick);
        joinBtt.onClick.RemoveListener(OnJoinBttClick);
    }

    private void OnNotificationClick()
    {
    }

    private void OnWalletClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.Wallet);
    }

    private void OnLudoClick()
    {
    }

    private void OnTeenPattiClick()
    {
    }

    private void OnCurrencyClick()
    {
        PlayerPrefs.SetInt("InGame", 1);
        SceneManager.LoadScene(1);
    }

    private void OnChessClick()
    {
    }

    private void OnJoinBttClick()
    {
        UIManager.LoadScreenAnimated(UIScreen.JoinGiveaway);
    }
}