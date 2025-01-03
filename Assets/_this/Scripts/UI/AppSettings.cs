using Christina.UI;
using HandyButtons;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZxLog;

public class AppSettings : MonoBehaviour
{
    [SerializeField] private ToggleSwitch music;
    [SerializeField] private ToggleSwitch sfx;
    [SerializeField] private ToggleSwitch vibration;
    [SerializeField] private Button back;

    [SerializeField] private bool bool1 = true; // Corresponds to music toggle
    [SerializeField] private bool bool2 = true; // Corresponds to sfx toggle
    [SerializeField] private bool bool3 = true;
    private void ToggleThing(ToggleSwitch type, bool value)
    {
        type.ToggleByGroupManager(value);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log(music.CurrentValue);
            Debug.Log(sfx.CurrentValue);
            Debug.Log(vibration.CurrentValue);
        }
    }

    [Button]
    private void TestToggleOn()
    {
        MusicToggle(true);
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
        // Set initial toggle states from user data
        GetSettingsFromUserData();
        //SetInitialToggleStates();
        //GetSettingsData();

        // Add listener for the back button
        back.onClick.AddListener(OnBack);
    }
    private void SetInitialToggleStates()
    {
        MusicToggle(bool1);  // Set music toggle based on bool1
        SfxToggle(bool2);    // Set sfx toggle based on bool2
        VibrationToggle(bool3); // Set vibration toggle based on bool3
    }

    private void OnDisable()
    {
        // Save updated settings on disable
        back.onClick.RemoveListener(OnBack);
        SaveSettings();
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    // Fetch user settings data and apply it to the toggles
    public void GetSettingsFromUserData()
    {
        var settingsUser = UserData.GetSettings(); 
        if (settingsUser != null)
        {
            MusicToggle(settingsUser.music);
            SfxToggle(settingsUser.soundEffect);
            VibrationToggle(settingsUser.vibration);
        }
    }

    // Save settings to UserData and post to the API
    private void SaveSettings()
    {
        var data = new Settings(music.CurrentValue, sfx.CurrentValue, vibration.CurrentValue);
        
        // If the current settings are different from the stored ones, save and post them
        if (UserData.GetSettings() != data)
        {
            UserData.SetSettings(data);
            ApiManager.Post<Settings, SettingResponseData>(ServiceURLs.UpdateSettings, data, OnSuccessSaveSettings, OnErrorSaveSettings);
        }
    }

    // Handle API success for settings update
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

    // Handle API error for settings update
    private void OnErrorSaveSettings(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    // Fetch the settings from the API if the stored settings differ
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
}
public class SettingResponseData
{
    public bool status;
    public string message;
    public Settings data;
}
