using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
public class SettingUI : UIBase
{
    [SerializeField] private InputReader _input;
    private VisualElement _settingRoot;
    private SliderInt _masterVolumeSlider, _musicVolumeSlider, _sfxVolumeSlider;
    private EnumField _screenEnumField;
    private bool _toggle;

    protected override void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }
    private void Start()
    {
        Debug.Log(SoundManager.Instance);
        _settingRoot = _doc.rootVisualElement.Q<VisualElement>("main_ui_setting_contain-box");

        _masterVolumeSlider = _settingRoot.Q<SliderInt>("main_ui_setting_master_volume-slider");
        _musicVolumeSlider = _settingRoot.Q<SliderInt>("main_ui_setting_music_volume-slider");
        _sfxVolumeSlider = _settingRoot.Q<SliderInt>("main_ui_setting_sfx_voluem-slider");
        _screenEnumField = _settingRoot.Q<EnumField>("main_ui_screen_type-field");


        _masterVolumeSlider.value = (int)(PlayerPrefs.GetFloat("MasterVolume") * 100);
        _masterVolumeSlider.RegisterCallback<ChangeEvent<int>>(evt =>
        {
            var value = Mathf.Max(evt.newValue * 0.01f, 0.01f);
            Debug.Log(SoundManager.Instance);
            SoundManager.Instance.VolumeSetMaster(value);
        });

        _musicVolumeSlider.value = (int)(PlayerPrefs.GetFloat("MusicVolume") * 100);
        _musicVolumeSlider.RegisterCallback<ChangeEvent<int>>(evt =>
        {
            var value = Mathf.Max(evt.newValue * 0.01f, 0.01f);

            SoundManager.Instance.VolumeSetMusic(value);
        });

        _sfxVolumeSlider.value = (int)(PlayerPrefs.GetFloat("SFXVoluem") * 100);
        _sfxVolumeSlider.RegisterCallback<ChangeEvent<int>>(evt =>
        {
            var value = Mathf.Max(evt.newValue * 0.01f, 0.01f);
            SoundManager.Instance.VolumeSetSFX(value);
        });
        _screenEnumField.RegisterCallback<ChangeEvent<Enum>>(evt =>
        {
            switch (evt.newValue)
            {
                case GameScreenModeType.전체화면:
                    Screen.fullScreen = true;
                    break;
                case GameScreenModeType.창모드:
                    Screen.fullScreen = false;
                    break;
            }
        });
    }
    public void SettingUIToggle()
    {
        if (_toggle)
        {
            _input.Console.Floor.Enable();
        }
        else
        {
            _input.Console.Floor.Disable();
        }
        _toggle = !_toggle;
        Cursor.visible = _toggle;
        Cursor.lockState = _toggle ? CursorLockMode.Confined : CursorLockMode.Locked;
        _settingRoot.ToggleInClassList("on");
    }


}
