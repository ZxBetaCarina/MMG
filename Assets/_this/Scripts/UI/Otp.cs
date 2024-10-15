using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Otp : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] otpFields;
    [SerializeField] private GameObject invalidText;
    [SerializeField] private Button verify;
    [SerializeField] private Button resend;
    [SerializeField] private Button back;
    
    public static event Action OnOtpVerified;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        resend.onClick.AddListener(OnResend);
        verify.onClick.AddListener(OnVerify);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        resend.onClick.RemoveListener(OnResend);
        verify.onClick.RemoveListener(OnVerify);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }

    private void OnResend()
    {
    }

    private void OnVerify()
    {
        OnOtpVerified.Invoke();
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    void Start()
    {
        for (int i = 0; i < otpFields.Length; i++)
        {
            int index = i;
            otpFields[i].onValueChanged.AddListener(delegate { OnFieldValueChanged(index); });
        }
    }

    void OnFieldValueChanged(int index)
    {
        if (otpFields[index].text.Length == 1 && index < otpFields.Length - 1)
        {
            otpFields[index + 1].ActivateInputField(); // Keeps the keyboard open while moving focus
        }

        if (otpFields[index].text == "" && index > 0)
        {
            otpFields[index - 1].ActivateInputField(); // Keeps the keyboard open when backspacing
        }

        // if (otpFields[index].isFocused && otpFields[index].text == "")
        // {
        //     ResetAllFields();
        //     otpFields[0].ActivateInputField();
        // }
    }

    void ResetAllFields()
    {
        foreach (TMP_InputField field in otpFields)
        {
            field.text = "";
        }
    }
}