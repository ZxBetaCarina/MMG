using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private Button signUp;
    [SerializeField] private Button accept;
    [SerializeField] private Button terms;
    [SerializeField] private Button policy;
    [SerializeField] private Toggle tncToggle;

    private void OnEnable()
    {
        accept.onClick.AddListener(OnAccept);
        terms.onClick.AddListener(OnTerms);
        policy.onClick.AddListener(OnPolicy);
        signUp.onClick.AddListener(OnSignUp);
    }

    private void OnDisable()
    {
        accept.onClick.RemoveListener(OnAccept);
        terms.onClick.RemoveListener(OnTerms);
        policy.onClick.RemoveListener(OnPolicy);
        signUp.onClick.RemoveListener(OnSignUp);
    }

    private void OnAccept()
    {
        if (tncToggle.isOn)
        {
            UIManager.LoadScreenAnimated(UIScreen.Otp);
        }
        else
        {
            var parent = tncToggle.transform.parent;
            parent.DOShakePosition(2f, new Vector3(500f, 0, 0), 1, 90, true, false);
        }
    }

    private void OnTerms()
    {
        UIManager.LoadScreenAnimated(UIScreen.TermsAndConditions);
    }

    private void OnPolicy()
    {
        UIManager.LoadScreenAnimated(UIScreen.PrivacyPolicy);
    }

    private void OnSignUp()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserDetails);
    }
}