using UnityEngine;
using UnityEngine.UI;

public class TermsAndConditions : MonoBehaviour
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
        if (UserData.IsUserLoggedIn)
        {
            UIManager.LoadScreenAnimated(UIScreen.Home);
        }
        else
        {
            UIManager.LoadScreenAnimated(UIScreen.SignIn);
        }
    }
}