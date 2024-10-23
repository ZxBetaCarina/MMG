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
        submit.interactable = false;
        if (message.text != string.Empty)
        {
            ApiManager.Post<SupportRequest, SupportResponseData>(ServiceURLs.Support, new SupportRequest(message
                .text), OnSuccess, OnError);
        }
    }

    private void OnSuccess(SupportResponseData obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.message);
            submit.interactable = true;
        }
        else
        {
            CustomLog.ErrorLog(obj.message);
            submit.interactable = true;
        }
    }

    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
        submit.interactable = true;
    }
}

public class SupportRequest
{
    public string message;

    public SupportRequest(string message)
    {
        this.message = message;
    }
}

public class SupportData
{
    public string userId { get; set; }
    public string message { get; set; }
    public string status { get; set; }
    public object dateResolved { get; set; }
    public string _id { get; set; }
    public DateTime dateSubmitted { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class SupportResponseData
{
    public bool status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
}