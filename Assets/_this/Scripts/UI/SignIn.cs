using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZxLog;

public class SignIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private Button accept;
    [SerializeField] private Button terms;
    [SerializeField] private Button policy;
    [SerializeField] private Toggle tncToggle;
    [SerializeField] private VerticalLayoutGroup layoutGroup;

    private void OnEnable()
    {
        accept.onClick.AddListener(OnAccept);
        terms.onClick.AddListener(OnTerms);
        policy.onClick.AddListener(OnPolicy);
    }

    private void OnDisable()
    {
        accept.onClick.RemoveListener(OnAccept);
        terms.onClick.RemoveListener(OnTerms);
        policy.onClick.RemoveListener(OnPolicy);
    }

    private void OnAccept()
    {
        if (tncToggle.isOn)
        {
            //UIManager.LoadScreenAnimated(UIScreen.Otp);
            if (email.text == String.Empty)
            {
                //popup
            }
            else
            {
                ApiManager.Post<SignInRequestData, SignInResponseData>(ServiceURLs.Login, new SignInRequestData(email
                    .text), OnSuccessSignIn, OnErrorSignIn);
            }
        }
        else
        {
            ShakeBox();
        }
    }

    private void OnSuccessSignIn(SignInResponseData obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.status.ToString() + obj.message);
            UIManager.LoadScreenAnimated(UIScreen.Otp);
        }
    }

    private void OnErrorSignIn(string obj)
    {
        CustomLog.ErrorLog(obj);
    }


    private void ShakeBox()
    {
        layoutGroup.enabled = false;

        LeanTween.cancel(tncToggle.transform.parent.gameObject); // Cancel any previous tweens

        LeanTween.value(tncToggle.transform.parent.gameObject, -7f, 7f, 0.2f)
            .setOnUpdate((float val) =>
            {
                tncToggle.transform.parent.localPosition = new Vector3(val,
                    tncToggle.transform.parent.localPosition.y, tncToggle.transform.parent.localPosition.z);
            })
            .setEase(LeanTweenType.easeShake)
            .setLoopPingPong(1).setOnComplete(() => layoutGroup.enabled = true);
    }

    private void OnTerms()
    {
        UIManager.LoadScreenAnimated(UIScreen.TermsAndConditions);
    }

    private void OnPolicy()
    {
        UIManager.LoadScreenAnimated(UIScreen.PrivacyPolicy);
    }
}

public class SignInRequestData
{
    public string email;

    public SignInRequestData(string email)
    {
        this.email = email;
    }
}

public class SignInResponseData
{
    public bool status;
    public string message;
}