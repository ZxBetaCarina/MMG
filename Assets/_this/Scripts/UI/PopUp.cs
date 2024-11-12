using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text head;
    [SerializeField] private TMP_Text message;
    [SerializeField] private Button ok;
    private Action onOkAction;

    private void OnEnable()
    {
        ok.onClick.AddListener(OnOk);
    }

    private void OnDisable()
    {
        ok.onClick.RemoveListener(OnOk);
    }

    private void OnOk()
    {
        gameObject.SetActive(false);
        //onOkAction?.Invoke(); // Invoke the method passed if it's not null
    }

    public void LoadPopUp(string head, string message)
    {
        this.head.text = head;
        this.message.text = message;
        onOkAction = null; // Reset the action
        gameObject.SetActive(true);
    }

    public void LoadPopUp(string head, string message, Action onOkAction)
    {
        this.head.text = head;
        this.message.text = message;
        this.onOkAction = onOkAction; // Assign the action
        gameObject.SetActive(true);
    }
}