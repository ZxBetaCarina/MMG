using Christina.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZxLog;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIScreen startScreen;
    [SerializeField] private DockManager dockManager;

    [SerializeField] private List<UIElement> uiElements;

    private static List<UIElement> _uiElementsStatic;
    private static float fadeDuration = 0.1f;
    private static DockManager _dock;
    private static UIManager _instance;
    public static Action _onbackbuttonpressed;
    private void Awake()
    {
        _uiElementsStatic = uiElements;
        _dock = dockManager;


        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        // Check if the back button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _onbackbuttonpressed?.Invoke();
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("InGame") == 1)
        {
            LoadScreenAnimated(UIScreen.Home);
            PlayerPrefs.SetInt("InGame", 0);
        }
        else
        {
            Print.Separator(LogColor.Red);
            LoadScreen(startScreen);
        }
        ApiManager.Initialize(this);
        ToggleSwitch.InitializeToggle(this);
    }

    private static void LoadScreen(UIScreen screen)
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
    // public static void ClearInputFields()
    // {
    //     foreach (var uiElement in _uiElementsStatic)
    //     {
    //         foreach (TMP_InputField field in uiElement.UserdataInputFields)
    //         {
    //             field.text = ""; // Clear the text of each input field
    //         }
    //     }
    // }
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
    Notifications,
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
    InsufficientCoinsPopUp,
    Withdraw,
    TermsAndConditions2,
    PrivacyPolicy2,
    BuyTicket,
    LudoBetUI,
    Deposit // add new screens according to needs
    
}

[Serializable]
public struct UIElement
{
    public UIScreen screenType;
    public GameObject screenObject;
  
}