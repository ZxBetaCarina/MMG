using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DockManager dockManager;
    [SerializeField] private Loading loading;
    [SerializeField] private PopUp popUp;
    [SerializeField] private List<UIElement> uiElements;

    private static List<UIElement> _uiElementsStatic;
    private static float fadeDuration = 0.5f;
    private static DockManager _dock;
    private static Loading _loading;
    private static PopUp _popUp;
    private static UIManager _instance;

    private void Awake()
    {
        _uiElementsStatic = uiElements;
        _dock = dockManager;
        _loading = loading;
        _popUp = popUp;

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        LoadScreen(UIScreen.Splash);
    }

    public static void LoadScreen(UIScreen screen)
    {
        DeactivateAllScreens();

        foreach (var uiElement in _uiElementsStatic)
        {
            if (uiElement.screenType == screen)
            {
                uiElement.screenObject.SetActive(true);
            }
        }
    }

    private static void DeactivateAllScreens()
    {
        foreach (var uiElement in _uiElementsStatic)
        {
            if (uiElement.screenObject.activeSelf)
            {
                uiElement.screenObject.SetActive(false);
            }
        }
    }

    public static void LoadScreenAnimated(UIScreen screen)
    {
        DeactivateAllScreensAnimated(); // First deactivate all screens
        _instance.StartCoroutine(ScreenLoader(screen));
    }
    
    private static IEnumerator ScreenLoader(UIScreen screen)
    {
        yield return new WaitForSeconds(fadeDuration);
        foreach (var uiElement in _uiElementsStatic)
        {
            if (uiElement.screenType == screen)
            {
                uiElement.screenObject.SetActive(true); // Activate the desired screen
                AppearanceManager.Singleton.FadeIn(uiElement.screenObject, fadeDuration, AnimationDirection.Up,
                    null); // Fade in the screen
            }
        }
    }

    private static void DeactivateAllScreensAnimated()
    {
        foreach (var uiElement in _uiElementsStatic)
        {
            if (uiElement.screenObject.activeSelf)
            {
                AppearanceManager.Singleton.FadeOut(uiElement.screenObject, fadeDuration, AnimationDirection.Down, () =>
                {
                    uiElement.screenObject.SetActive(false); // Deactivate after fade out
                });
            }
        }
    }

    public static void ShowLoading(bool value)
    {
        _loading.gameObject.SetActive(value);
    }

    public static void ShowPopUp(string head, string body)
    {
        _popUp.LoadPopUp(head, body);
    }

    public static void Button_Activate_Dock_Button_And_Screen(int p_index)
    {
        _dock.ActivateDockButtonAndScreen(p_index);
    }
}

public enum UIScreen
{
    Splash,
    SignIn,
    UserDetails,
    Otp,
    Home,
    Wallet,
    QrCode,
    MyTickets,
    Transaction,
    AppSettings,
    UserProfile,
    EditUserProfile,
    JoinGiveaway,
    TermsAndConditions,
    PrivacyPolicy,
    RateUs,
    LeaderBoards,
    Support,
    ExchangeCoins,
    RatePopUp,
    InsufficientCoinsPopUp, // add new screens according to needs
}

[Serializable]
public struct UIElement
{
    public UIScreen screenType;
    public GameObject screenObject;
}