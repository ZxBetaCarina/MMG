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

    private void OnEnable()
    {
        home.onClick.AddListener(() => ActivateDockButtonAndScreen(0));
        refer.onClick.AddListener(() => ActivateDockButtonAndScreen(1));
        game.onClick.AddListener(() => ActivateDockButtonAndScreen(2));
        account.onClick.AddListener(() => ActivateDockButtonAndScreen(3));
    }

    private void OnDisable()
    {
        home.onClick.RemoveListener(() => ActivateDockButtonAndScreen(0));
        refer.onClick.RemoveListener(() => ActivateDockButtonAndScreen(1));
        game.onClick.RemoveListener(() => ActivateDockButtonAndScreen(2));
        account.onClick.RemoveListener(() => ActivateDockButtonAndScreen(3));
    }

    public void ActivateDockButtonAndScreen(int p_index)
    {
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
            GameObject t_selected_screen_object = dockScreensParent.GetChild(i).gameObject;
            if (i != p_index)
            {
                AppearanceManager.Singleton.FadeOut(t_selected_screen_object, fadeDuration, AnimationDirection.Down,
                    () => t_selected_screen_object.SetActive(false));
            }
        }

        StartCoroutine(Cor_ActivateDockButtonAndScreen(p_index));
    }

    private IEnumerator Cor_ActivateDockButtonAndScreen(int p_index)
    {
        yield return new WaitForSeconds(fadeDuration);
        GameObject t_selected_screen_object = dockScreensParent.GetChild(p_index).gameObject;
        t_selected_screen_object.SetActive(true);
        AppearanceManager.Singleton.FadeIn(t_selected_screen_object, fadeDuration, AnimationDirection.Up);
    }
}