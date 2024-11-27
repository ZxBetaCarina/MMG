using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUi : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private GameObject betScreen;

    private void OnEnable()
    {
        play.onClick.AddListener(OnPlayClick);
    }

    private void OnDisable()
    {
        play.onClick.RemoveListener(OnPlayClick);
    }
    private void OnPlayClick()
    {
        gameObject.SetActive(false);
        betScreen.SetActive(true);
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }
}