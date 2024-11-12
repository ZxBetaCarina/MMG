using System;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private Loading loading;
    [SerializeField] private PopUp popUp;
    private static Loading _loading;
    private static PopUp _popUp;
    private static PopUpManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void InitializeStatics()
    {
        if (_instance == null) return;

        if (_loading == null) _loading = _instance.loading;
        if (_popUp == null) _popUp = _instance.popUp;
    }

    public static void ShowLoading(bool value)
    {
        InitializeStatics();
        if (_loading != null)
        {
            _loading.gameObject.SetActive(value);
        }
    }

    public static void ShowPopUp(string head, string body)
    {
        InitializeStatics();
        if (_popUp != null)
        {
            _popUp.LoadPopUp(head, body);
        }
    }

    public static void ShowPopUpAction(string head, string body, Action action)
    {
        InitializeStatics();
        if (_popUp != null)
        {
            _popUp.LoadPopUp(head, body, action);
        }
    }
}