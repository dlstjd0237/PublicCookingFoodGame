using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class InGameUI : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    private SettingUI _settingUI;
    private UIDocument _doc;
    private void Start()
    {
        _settingUI = GetComponent<SettingUI>();
        _doc = GetComponent<UIDocument>();
        _doc.rootVisualElement.Q<Button>("main_menu-btn").RegisterCallback<ClickEvent>(evt => SceneControlManager.FadeOut(() => SceneManager.LoadScene(0)));
    }

    private void OnEnable()
    {
        _input.EsckeyEvent += HandleOptionEvent;
    }

    private void HandleOptionEvent()
    {
        _settingUI.SettingUIToggle();
    }

    private void OnDisable()
    {
        _input.EsckeyEvent -= HandleOptionEvent;
    }

}
