using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Support : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private TMP_InputField message;
    [SerializeField] private Button submit;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        submit.onClick.AddListener(OnSubmit);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        submit.onClick.RemoveListener(OnSubmit);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnSubmit()
    {
    }
}