using UnityEngine;
using UnityEngine.UI;

public class MyTickets : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private Transform ticketParent;

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
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }
}