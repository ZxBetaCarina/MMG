using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUi : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button BackBtn;
    [SerializeField] private GameObject betScreen;

    private void OnEnable()
    {
        play.onClick.AddListener(OnPlayClick);
        BackBtn.onClick.AddListener(OnBackClick);
    }

    private void OnDisable()
    {
        play.onClick.RemoveListener(OnPlayClick);
        BackBtn.onClick.RemoveListener(OnBackClick);
    }
    private void OnPlayClick()
    {
        gameObject.SetActive(false);
        betScreen.SetActive(true);
    }
    private void OnBackClick()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }
}