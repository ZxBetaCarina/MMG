using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class UIManagement : MonoBehaviour
{
    [SerializeField] Transform _screens_parent_transform;
    [SerializeField] List<Sprite> _off_on_sprite;


    [Header("Dock")]
      // Reference to the DockManager
    // [SerializeField] Image _dock_bg;
    // [SerializeField] List<Sprite> _dock_bg_sprite_list;
    // [SerializeField] List<Image> _dock_active_button_list;
    // [SerializeField] List<Button> _dock_inactive_button_list;
    // [SerializeField] Transform _dock_screens_parent;

    [Header("QR TEMP")]
    [SerializeField] GameObject _qr_overlay_object;
    [SerializeField] GameObject _qr_overlay_popup_object;
    
    [Header("OTP")]
    [SerializeField] List<TMP_InputField> _otp_input_field_list;
    
    [Header("User")]
    [SerializeField] List<Sprite> _radio_button_off_on_sprite_list;
    [SerializeField] List<Image> _radio_button_image_list;

    Color _qr_overlay_colour;
    int _last_dock;
    string _last_input = "";
    bool _is_formatting;

    // IEnumerator Start()
    // {
    //     _qr_overlay_colour = _qr_overlay_object.GetComponent<Image>().color;
    //     _splash_image.color = new Color(_splash_image.color.r, _splash_image.color.g, _splash_image.color.b, 0);
    //     yield return new WaitForSeconds(_splash_duration);
    //     AppearanceManager._AM.Fade_game_object_in(_splash_image.gameObject, _splash_duration, AnimationDirection.Up, () =>
    //     {
    //         StartCoroutine(Cor_splash_out());
    //     });
    // }
    
    // public void Button_Activate_Screen(int p_index)
    // {
    //     if (p_index == 1)
    //     {
    //         _last_dock = 0;
    //     }
    //
    //     if (p_index == 4)
    //     {
    //         float t_fade_duration = _fade_duration;
    //         _fade_duration = 0.0001f;
    //         Button_Activate_Dock_Button_And_Screen(_last_dock);
    //         _fade_duration = t_fade_duration;
    //     }
    //
    //     //  fade out existing screen first
    //     for (int i = 0; i < _screens_parent_transform.childCount; i++)
    //     {
    //         if (i == p_index)
    //         {
    //             continue;
    //         }
    //         else if (_screens_parent_transform.GetChild(i).gameObject.activeInHierarchy)
    //         {
    //             AppearanceManager._AM.Fade_game_object_out(_screens_parent_transform.GetChild(i).gameObject, _fade_duration, AnimationDirection.Down, () =>
    //             {
    //                 _screens_parent_transform.GetChild(i).gameObject.SetActive(false);
    //                 StartCoroutine(Cor_activate_screen(p_index));
    //             });
    //             break;
    //         }
    //     }
    // }
    //
    // IEnumerator Cor_activate_screen(int p_index)
    // {
    //     yield return new WaitForSeconds(_fade_duration);
    //     GameObject t_selected_screen_object = _screens_parent_transform.GetChild(p_index).gameObject;
    //     t_selected_screen_object.SetActive(true);
    //     AppearanceManager._AM.Fade_game_object_in(t_selected_screen_object, _fade_duration, AnimationDirection.Up);
    // }

    // public void Button_Activate_Dock_Button_And_Screen(int p_index)
    // {
    //     _last_dock = p_index;
    //     //  active buttons
    //     for (int i = 0; i < _dock_active_button_list.Count; i++)
    //     {
    //         int t_index = i;
    //         if (i != p_index)
    //         {
    //             AppearanceManager._AM.Fade_game_object_out(_dock_active_button_list[i].gameObject, _fade_duration, AnimationDirection.Down, () => _dock_active_button_list[t_index].enabled = false);
    //         }
    //     }
    //
    //     _dock_active_button_list[p_index].enabled = true;
    //     AppearanceManager._AM.Fade_game_object_in(_dock_active_button_list[p_index].gameObject, _fade_duration, AnimationDirection.Up);
    //
    //     //  dock bg
    //     _dock_bg.sprite = _dock_bg_sprite_list[p_index];
    //
    //     //  inactive buttons
    //     for (int i = 0; i < _dock_inactive_button_list.Count; i++)
    //     {
    //         int t_index = i;
    //         if (i != p_index && !_dock_inactive_button_list[t_index].interactable)
    //         {
    //             _dock_inactive_button_list[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
    //             AppearanceManager._AM.Fade_game_object_in(_dock_inactive_button_list[i].transform.GetChild(0).gameObject, _fade_duration, AnimationDirection.Up, () =>
    //             {
    //                 _dock_inactive_button_list[t_index].interactable = true;
    //             });
    //         }
    //     }
    //
    //     _dock_inactive_button_list[p_index].transform.GetChild(0).GetComponent<Image>().enabled = false;
    //     _dock_inactive_button_list[p_index].interactable = false;
    //     AppearanceManager._AM.Fade_game_object_out(_dock_inactive_button_list[p_index].gameObject, _fade_duration, AnimationDirection.Down, () =>
    //     {
    //
    //     });
    //
    //     //  dock screens
    //     for (int i = 0; i < _dock_screens_parent.childCount; i++)
    //     {
    //         GameObject t_selected_screen_object = _dock_screens_parent.GetChild(i).gameObject;
    //         if (i != p_index)
    //         {
    //             AppearanceManager._AM.Fade_game_object_out(t_selected_screen_object, _fade_duration, AnimationDirection.Down, () => t_selected_screen_object.SetActive(false));
    //         }
    //     }
    //     StartCoroutine(Cor_activate_dock_button_and_screen(p_index));
    // }
    //
    // IEnumerator Cor_activate_dock_button_and_screen(int p_index)
    // {
    //     yield return new WaitForSeconds(_fade_duration);
    //     GameObject t_selected_screen_object = _dock_screens_parent.GetChild(p_index).gameObject;
    //     t_selected_screen_object.SetActive(true);
    //     AppearanceManager._AM.Fade_game_object_in(t_selected_screen_object, _fade_duration, AnimationDirection.Up);
    // }

    // public void Button_QR_Overlay()
    // {
    //     _qr_overlay_object.GetComponent<Image>().color = _qr_overlay_colour;
    //     _qr_overlay_object.SetActive(true);
    //     AppearanceManager._AM.Fade_game_object_in(_qr_overlay_popup_object, _fade_duration, AnimationDirection.Left);
    // }
    
    // public void Button_Reset_QR()
    // {
    //     _qr_overlay_object.SetActive(false);
    // }

    public void Button_ON_OFF()
    {
        Image t_button_selected = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>();
        if (t_button_selected.sprite == _off_on_sprite[0])
        {
            t_button_selected.sprite = _off_on_sprite[1];
        }
        else
        {
            t_button_selected.sprite = _off_on_sprite[0];
        }
    }

    // public void Button_OTP_Input_Field_Change(int p_index)
    // {
    //     if (p_index < _otp_input_field_list.Count - 1)
    //     {
    //         _otp_input_field_list[p_index + 1].ActivateInputField();
    //     }
    //     else
    //     {
    //         _otp_input_field_list[p_index].DeactivateInputField();
    //     }
    // }
    //
    // public void Button_User_Details_Radio_Buttons(int p_index)
    // {
    //     _radio_button_image_list.ForEach(button => button.sprite = _radio_button_off_on_sprite_list[0]);
    //     _radio_button_image_list[p_index].sprite = _radio_button_off_on_sprite_list[1];
    // }

    public void Button_Format_DOB_Input(string input)
    {
        TMP_InputField t_if = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        // Check if backspace was pressed
        if (input.Length < _last_input.Length)
        {
            _last_input = input;
            return;
        }

        if (_is_formatting && input.Length <= _last_input.Length)
        {
            _last_input = input;
            return;
        }

        _last_input = input;

        switch (input.Length)
        {
            case 1:
                if (int.Parse(input[..1]) > 3)
                {
                    t_if.text = "";
                }
                break;
            case 2:
                if ((int.Parse(input[..1]) == 3 && int.Parse(input.Substring(1, 1)) > 1) ||
                    (int.Parse(input[..1]) == 0 && int.Parse(input.Substring(1, 1)) < 1))
                {
                    t_if.text = input[..1];
                }
                else
                {
                    _is_formatting = true;
                    t_if.text = input.Insert(2, "/");
                    StartCoroutine(SetCaretPositionDelayed(t_if.text.Length, t_if));
                }
                break;
            case 3:
                if (input.Substring(2, 1) == "/")
                {
                    return;
                }
                else
                {
                    _is_formatting = true;
                    t_if.text = input.Insert(2, "/");
                    StartCoroutine(SetCaretPositionDelayed(t_if.text.Length, t_if));
                }
                break;
            case 4:
                if (int.Parse(input.Substring(3, 1)) > 1)
                {
                    t_if.text = input[..3];
                }
                break;
            case 5:
                if (!IsValidDayForMonth(int.Parse(input.Substring(0, 2)), int.Parse(input.Substring(3, 2)), 2024))
                {
                    t_if.text = input[..4];
                }
                else
                {
                    _is_formatting = true;
                    t_if.text = input.Insert(5, "/");
                    StartCoroutine(SetCaretPositionDelayed(t_if.text.Length, t_if));
                }
                break;
            case 6:
                if (input.Substring(5, 1) == "/")
                {
                    return;
                }
                else
                {
                    _is_formatting = true;
                    t_if.text = input.Insert(5, "/");
                    StartCoroutine(SetCaretPositionDelayed(t_if.text.Length, t_if));
                }
                break;
            case 7:
                if (int.Parse(input.Substring(6, 1)) > DateTime.Now.Year / 1000)
                {
                    t_if.text = input[..6];
                }
                break;
            case 8:
                if (int.Parse(input.Substring(6, 1)) == DateTime.Now.Year / 1000 && int.Parse(input.Substring(7, 1)) > (DateTime.Now.Year / 100) % 10)
                {
                    t_if.text = input[..7];
                }
                break;
            case 9:
                if (int.Parse(input.Substring(6, 1)) == DateTime.Now.Year / 1000 && int.Parse(input.Substring(7, 1)) == (DateTime.Now.Year / 100) % 10 && int.Parse(input.Substring(8, 1)) > (DateTime.Now.Year / 10) % 10)
                {
                    t_if.text = input[..7];
                }
                break;
            case 10:
                if (!IsValidDayForMonth(int.Parse(input.Substring(0, 2)), int.Parse(input.Substring(3, 2)), int.Parse(input.Substring(6, 4))))
                {
                    t_if.text = input[..9];
                }
                break;
            case int n when n > 10:
                t_if.text = input[..10];
                break;
        }
    }

    IEnumerator SetCaretPositionDelayed(int position, TMP_InputField inputField)
    {
        yield return null;  // Wait for the next frame
        inputField.caretPosition = position;
        inputField.ForceLabelUpdate();
        _is_formatting = false;
    }

    private bool IsValidDayForMonth(int day, int month, int year)
    {
        if (year > DateTime.Now.Year || year < 1900 || month > 12 || month < 1)
        {
            return false;
        }

        // Handle the number of days in each month
        int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        // Check if it's a leap year for February
        if (month == 2 && IsLeapYear(year))
        {
            return day <= 29;
        }

        return day <= daysInMonth[month - 1];
    }

    private bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
