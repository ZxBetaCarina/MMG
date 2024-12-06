using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    [SerializeField] private Button back;
    

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
    }
    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

}