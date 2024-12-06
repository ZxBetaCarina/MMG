using UnityEngine;
using UnityEngine.UI;

public class MyTickets : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private Transform ticketParent;

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }
}