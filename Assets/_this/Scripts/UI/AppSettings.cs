using Christina.UI;
using HandyButtons;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AppSettings : MonoBehaviour
{
    [SerializeField] private ToggleSwitch music;
    [SerializeField] private ToggleSwitch sfx;
    [SerializeField] private ToggleSwitch vibration;
    [SerializeField] private Button back;

    private bool defaultMusic = true;
    private bool defaultSfx = true;
    private bool defaultVibration = true;

    private void Start()

    private void ToggleThing(ToggleSwitch type, bool value)
    {
        if (value)
        {
            if (!music.CurrentValue)
            {
                type.Toggle();
            }
        }
        else
        {
            if (music.CurrentValue)
            {
                type.Toggle();
            }
        }
    }

    [Button]
    private void TestToggleOn()
    {
        MusicToggle(true);// THIS ONE SHOULD BE ALLED ASLO IN GameStarting
    }
    
    [Button]
    private void TestToggleOff()
    {
        MusicToggle(false);
    }
    private void MusicToggle(bool value)
    {
        ToggleThing(music, value);
    }

    private void SfxToggle(bool value)
    {
        ToggleThing(sfx, value);
    }

    private void VibrationToggle(bool value)
    {
        ToggleThing(vibration, value);
    }

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
       // GetSettingsData();

    }
    public void GetSettingsFromUserData()
    {
        var settingsUser = UserData.GetSettings(); 
        var data = new Settings(music.CurrentValue, sfx.CurrentValue, vibration.CurrentValue);
        if (data != settingsUser)
        {
            MusicToggle(settingsUser.music);
            SfxToggle(settingsUser.soundEffect);
            VibrationToggle(settingsUser.vibration);
        }
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        SaveSettings();
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void GetSettingsData()
    {
        Settings data = new Settings(music.CurrentValue, sfx.CurrentValue, vibration.CurrentValue);
        if (UserData.GetSettings() != data)
        {
            UserData.SetSettings(data);
            ApiManager.Get<SettingResponseData>(ServiceURLs.GetSettings, OnSuccess, OnError);
        }
    }
    private void OnSuccess(SettingResponseData obj)
    {
        if (obj.status)
        {
            MusicToggle(obj.data.music);
            SfxToggle(obj.data.soundEffect);
            VibrationToggle(obj.data.vibration);
        }
        else
        {
            CustomLog.ErrorLog(obj.message);
        }
    }
    private void OnError(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    private void SaveSettings()
    {
        var data = new Settings(music.CurrentValue, sfx.CurrentValue, vibration.CurrentValue);
        if (UserData.GetSettings() != data)
        {
            UserData.SetSettings(data);
            ApiManager.Post<Settings, SettingResponseData>(ServiceURLs.UpdateSettings, data, OnSuccessSaveSettings, OnErrorSaveSettings);
        }
    }
    private void OnSuccessSaveSettings(SettingResponseData obj)
    {
        if (obj.status)
        {
            CustomLog.SuccessLog(obj.message);
        }
        else
        {
            CustomLog.ErrorLog(obj.message);
        }
    }
    private void OnErrorSaveSettings(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
}

public class SettingResponseData
{
    public bool status;
    public string message;
    public Settings data;
}
