using HandyButtons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Button> allButtons = new List<Button>();
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private bool vibration;
    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (allButtons.Count == 0)
        {
            GetButtons();
        }
    }

    [Button]
    private void GetButtons()
    {
        allButtons.Clear();
        Button[] buttons = FindObjectsOfType<Button>(true);
        allButtons.AddRange(buttons);
    }

    private void OnEnable()
    {
        SetupButtonListeners(true);
    }
    private void OnDisable()
    {
        SetupButtonListeners(false);
    }

    private void SetupButtonListeners(bool value)
    {
        if (value)
        {
            foreach (Button button in allButtons)
            {
                button.onClick.AddListener(PlaySfx);
            }
        }
        else
        {
            foreach (Button button in allButtons)
            {
                button.onClick.RemoveListener(PlaySfx);
            }
        }
    }

    private void PlaySfx()
    {
        sfx.Play();
    }

    public static void PlayMusic(bool value)
    {
        _instance.music.mute = !value;

        if (value)
        {
            if (!_instance.music.isPlaying)
            {
                _instance.music.Play();
            }
        }
        else
        {
            if (_instance.music.isPlaying)
            {
                _instance.music.Stop();
            }
        }
    }
    
    public static void MuteSfx(bool value)
    {
        _instance.sfx.mute = value;
    }

    public static void ToggleVibration(bool value)
    {
        _instance.vibration = value;
    }

    //call this method for vibration
    public static void Vibrate()
    {
        if (!_instance.vibration) return;
        Handheld.Vibrate();
    }
}
