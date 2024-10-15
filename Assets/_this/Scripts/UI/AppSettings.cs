using UnityEngine;
using UnityEngine.UI;

public class AppSettings : MonoBehaviour
{
    [SerializeField] private Slider music;
    [SerializeField] private Slider effect;
    [SerializeField] private Slider vibrations;
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
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }
}