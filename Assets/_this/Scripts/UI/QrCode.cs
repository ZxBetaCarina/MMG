using System;
using UnityEngine;
using UnityEngine.UI;

public class QrCode : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private Button sendReq;
    [SerializeField] private Image qrCode;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        sendReq.onClick.AddListener(OnSendReq);
        qrCode.sprite = UserData.GetQrCode();
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        sendReq.onClick.RemoveListener(OnSendReq);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnSendReq()
    {
        ApiManager.Post<SendResponseData>(ServiceURLs.SendRequest, OnSuccess, OnError);
    }

    private void OnSuccess(SendResponseData obj)
    {
        if (obj.status)
        {
            UIManager.ShowPopUp("Request sent successfully",
                " Thank you for requesting a treasure ticket. Our sales executive will contact you shortly. \n\n Thanks for your cooperation.");
        }
    }

    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class SendData
{
    public string gameUserId { get; set; }
    public bool isCopied { get; set; }
    public string _id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class SendResponseData
{
    public bool status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
}