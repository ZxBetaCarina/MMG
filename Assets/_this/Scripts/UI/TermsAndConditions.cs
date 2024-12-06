using UnityEngine;
using UnityEngine.UI;

public class TermsAndConditions : MonoBehaviour
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
    public void backtosignin()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }
}