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
    [SerializeField] Image _dock_bg;
    [SerializeField] List<Sprite> _dock_bg_sprite_list;
    [SerializeField] List<Image> _dock_active_button_list;
    [SerializeField] List<Button> _dock_inactive_button_list;
    [SerializeField] Transform _dock_screens_parent;
    [SerializeField] float _fade_duration;

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
        for (int i = 0; i < _dock_active_button_list.Count; i++)
        {
            int t_index = i;
            if (i != p_index)
            {
                AppearanceManager.Singleton.FadeOut(_dock_active_button_list[i].gameObject, _fade_duration, AnimationDirection.Down, () => _dock_active_button_list[t_index].enabled = false);
            }
        }

        _dock_active_button_list[p_index].enabled = true;
        AppearanceManager.Singleton.FadeIn(_dock_active_button_list[p_index].gameObject, _fade_duration, AnimationDirection.Up);

        // Dock background
        _dock_bg.sprite = _dock_bg_sprite_list[p_index];

        // Inactive buttons
        for (int i = 0; i < _dock_inactive_button_list.Count; i++)
        {
            int t_index = i;
            if (i != p_index && !_dock_inactive_button_list[t_index].interactable)
            {
                _dock_inactive_button_list[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                AppearanceManager.Singleton.FadeIn(_dock_inactive_button_list[i].transform.GetChild(0).gameObject, _fade_duration, AnimationDirection.Up, () =>
                {
                    _dock_inactive_button_list[t_index].interactable = true;
                });
            }
        }

        _dock_inactive_button_list[p_index].transform.GetChild(0).GetComponent<Image>().enabled = false;
        _dock_inactive_button_list[p_index].interactable = false;
        AppearanceManager.Singleton.FadeOut(_dock_inactive_button_list[p_index].gameObject, _fade_duration, AnimationDirection.Down, null);

        // Dock screens
        for (int i = 0; i < _dock_screens_parent.childCount; i++)
        {
            GameObject t_selected_screen_object = _dock_screens_parent.GetChild(i).gameObject;
            if (i != p_index)
            {
                AppearanceManager.Singleton.FadeOut(t_selected_screen_object, _fade_duration, AnimationDirection.Down, () => t_selected_screen_object.SetActive(false));
            }
        }

        StartCoroutine(Cor_ActivateDockButtonAndScreen(p_index));
    }

    private IEnumerator Cor_ActivateDockButtonAndScreen(int p_index)
    {
        yield return new WaitForSeconds(_fade_duration);
        GameObject t_selected_screen_object = _dock_screens_parent.GetChild(p_index).gameObject;
        t_selected_screen_object.SetActive(true);
        AppearanceManager.Singleton.FadeIn(t_selected_screen_object, _fade_duration, AnimationDirection.Up);
    }
}
