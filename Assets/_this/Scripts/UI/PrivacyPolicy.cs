using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicy : MonoBehaviour
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
    public void Backtosignin()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }

    private void GetPrivacyPolicy()
    {
        ApiManager.Get<PrivacyPolicyResponseData>(ServiceURLs.GetTermsPolicy, OnSuccessGetPrivacyPolicy,
            OnErrorGetPrivacyPolicy);
    }

    private void OnSuccessGetPrivacyPolicy(PrivacyPolicyResponseData obj)
    {
    }

    private void OnErrorGetPrivacyPolicy(string obj)
    {
        CustomLog.ErrorLog("Nothing to parse to");
    }
}

public class PrivacyPolicyResponseData
{
}