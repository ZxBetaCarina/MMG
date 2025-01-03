using System.Collections.Generic;
using HandyButtons;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Button> allButtons = new List<Button>();
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private bool vibration;
    public static AudioManager _instance;
   
    public AppSettings _AppSettings; // Assuming you are injecting or accessing AppSettings

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
        Profile.OnProfileLoaded += OnProfileLoaded;  // Subscribe to the profile loaded event
    }

    private void OnDisable()
    {
        SetupButtonListeners(false);
        Profile.OnProfileLoaded -= OnProfileLoaded;  // Unsubscribe to avoid memory leaks
    }

    private void OnProfileLoaded()
    {
        // Fetch settings from user data and apply them
        var settingsUser = UserData.GetSettings();  // Assuming UserData.GetSettings() gives the settings

        if (settingsUser != null)
        {
            // Apply the settings to the audio
            ApplyAudioSettings(settingsUser);
        }
    }

    private void ApplyAudioSettings(Settings settings)
    {
        // Apply Music setting
        PlayMusic(settings.music);

        // Apply SFX setting
        MuteSfx(!settings.soundEffect);  // If soundEffect is true, mute will be false and vice versa

        // Apply Vibration setting
        ToggleVibration(settings.vibration);
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

    public AudioSource GetMusicAudioSource()
    {
        return music;
    }

    public AudioSource GetsfxAudioSource()
    {
        return sfx;
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

    // Call this method for vibration
    public static void Vibrate()
    {
        if (!_instance.vibration) return;
        Handheld.Vibrate();
    }
}
