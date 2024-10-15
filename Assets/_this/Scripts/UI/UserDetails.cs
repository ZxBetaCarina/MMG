using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserDetails : MonoBehaviour
{
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField number;
    [SerializeField] private TMP_InputField dob;
    [SerializeField] private TMP_InputField refer;
    [SerializeField] private TMP_InputField location;
    [SerializeField] private ToggleGroup gender;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button back;

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinue);
        back.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(OnContinue);
        back.onClick.RemoveListener(OnBack);
    }

    private void OnContinue()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }
}