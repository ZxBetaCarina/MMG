using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text head;
    [SerializeField] private TMP_Text message;
    [SerializeField] private Button ok;

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
    }

    public void LoadPopUp(string head, string message)
    {
        this.head.text = head;
        this.message.text = message;
        gameObject.SetActive(true);
    }
}