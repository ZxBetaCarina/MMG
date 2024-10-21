﻿using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicy : MonoBehaviour
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