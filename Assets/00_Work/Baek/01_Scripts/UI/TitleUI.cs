using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class TitleUI : UIBase
{
    private VisualElement _titleScene;
    private VisualElement _mainUI;

    private void Start()
    {
        SoundManager.Instance.MixerNullChake();
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        _mainUI = _doc.rootVisualElement.Q<VisualElement>("ui_contain-box");
        _titleScene = _doc.rootVisualElement.Q<VisualElement>("title_scene-box");
        _titleScene.RegisterCallback<ClickEvent>(evt => { _titleScene.AddToClassList("on"); _mainUI.RemoveFromClassList("on"); });
    }

    public void TitleOn()
    {
        _titleScene.RemoveFromClassList("on");
    }
}
