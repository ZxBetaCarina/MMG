using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockManager : MonoBehaviour
{
    [SerializeField] private Button home;
    [SerializeField] private Button refer;
    [SerializeField] private Button game;
    [SerializeField] private Button account;
    [SerializeField] private Image dockBg;
    [SerializeField] private List<Sprite> dockBgSpriteList;
    [SerializeField] private List<Image> dockActiveButtonList;
    [SerializeField] private List<Button> dockInactiveButtonList;
    [SerializeField] private Transform dockScreensParent;
    [SerializeField] private float fadeDuration;

    private int _last_dock;
    private bool isAnimating; // Flag to prevent overlapping animations

    private void Start()
    {
        // Disable the home button initially
        home.interactable = false;
        home.transform.GetChild(0).GetComponent<Image>().enabled = false; // Hide its associated image if needed
    }
    private void OnEnable()
    {
        home.onClick.AddListener(() => AttemptActivateDockButtonAndScreen(0));
        refer.onClick.AddListener(() => AttemptActivateDockButtonAndScreen(1));
        game.onClick.AddListener(() => AttemptActivateDockButtonAndScreen(2));
        account.onClick.AddListener(() => AttemptActivateDockButtonAndScreen(3));
    }

    private void OnDisable()
    {
        home.onClick.RemoveListener(() => AttemptActivateDockButtonAndScreen(0));
        refer.onClick.RemoveListener(() => AttemptActivateDockButtonAndScreen(1));
        game.onClick.RemoveListener(() => AttemptActivateDockButtonAndScreen(2));
        account.onClick.RemoveListener(() => AttemptActivateDockButtonAndScreen(3));
    }

    private void AttemptActivateDockButtonAndScreen(int p_index)
    {
        if (TrailPeriod.instance.isTrailPeriod && (p_index == 1 || p_index == 2))
        {
            PopUpManager.ShowPopUp("Message", " This section is locked For you.for unlocking all features please buy 1 Ticket.");
            return ;
        }
        if (isAnimating) return; // Prevent multiple calls if already animating
    
        // Enable the home button only if another button is activated
        if (_last_dock == -1 && p_index != 0)
        {
            home.interactable = true;
            home.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        StartCoroutine(ActivateDockButtonAndScreen(p_index));
    }

    public IEnumerator ActivateDockButtonAndScreen(int p_index)
    {
        isAnimating = true; // Set flag to true at the start of the animation
        _last_dock = p_index;

        // Active buttons
        for (int i = 0; i < dockActiveButtonList.Count; i++)
        {
            int t_index = i;
            if (i != p_index)
            {
                AppearanceManager.Singleton.FadeOut(dockActiveButtonList[i].gameObject, fadeDuration,
                    AnimationDirection.Down, () => dockActiveButtonList[t_index].enabled = false);
            }
        }

        dockActiveButtonList[p_index].enabled = true;
        AppearanceManager.Singleton.FadeIn(dockActiveButtonList[p_index].gameObject, fadeDuration,
            AnimationDirection.Up);

        // Dock background
        dockBg.sprite = dockBgSpriteList[p_index];

        // Inactive buttons
        for (int i = 0; i < dockInactiveButtonList.Count; i++)
        {
            int t_index = i;
            if (i != p_index && !dockInactiveButtonList[t_index].interactable)
            {
                dockInactiveButtonList[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                AppearanceManager.Singleton.FadeIn(dockInactiveButtonList[i].transform.GetChild(0).gameObject,
                    fadeDuration, AnimationDirection.Up,
                    () => { dockInactiveButtonList[t_index].interactable = true; });
            }
        }

        dockInactiveButtonList[p_index].transform.GetChild(0).GetComponent<Image>().enabled = false;
        dockInactiveButtonList[p_index].interactable = false;
        AppearanceManager.Singleton.FadeOut(dockInactiveButtonList[p_index].gameObject, fadeDuration,
            AnimationDirection.Down, null);

        // Dock screens
        for (int i = 0; i < dockScreensParent.childCount; i++)
        {
            GameObject screenObject = dockScreensParent.GetChild(i).gameObject;
            if (i != p_index)
            {
                AppearanceManager.Singleton.FadeOut(screenObject, fadeDuration, AnimationDirection.Down,
                    () => screenObject.SetActive(false));
            }
        }

        yield return new WaitForSeconds(fadeDuration); // Wait for the fade duration
        GameObject selectedScreenObject = dockScreensParent.GetChild(p_index).gameObject;
        selectedScreenObject.SetActive(true);
        AppearanceManager.Singleton.FadeIn(selectedScreenObject, fadeDuration, AnimationDirection.Up);

        // Reset the animation flag after finishing
        yield return new WaitForSeconds(fadeDuration); // Wait for the fade-in to complete
        isAnimating = false;
    }
}
