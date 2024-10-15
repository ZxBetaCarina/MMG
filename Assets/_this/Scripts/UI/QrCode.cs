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
        UIManager.ShowPopUp("Request sent successfully",
            " Thank you for requesting a treasure ticket. Our sales executive will contact you shortly. \n\n Thanks for your cooperation.");
    }
}