using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainSceneUI : UIBase
{
    [SerializeField] private Color _choiceColor, _unChoiceColor;
    public UnityEvent ChapterChoiceEvent;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;

    #region UIcontain

    private VisualElement _tabBarRoot;
    private VisualElement _mainUIRoot;
    private Button _tutorialBtn;
    private Button _chapterLevelOneBtn;
    private Button _chapterArrBtn;
    private Button _settingBtn;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();

    }
    private void Start()
    {
        _tabBarRoot = _doc.rootVisualElement.Q<VisualElement>("main_ui_tabbar_contain-box");
        _mainUIRoot = _doc.rootVisualElement.Q<VisualElement>("main_ui_contain-box");
        _audioSource = GetComponent<AudioSource>();
        _tutorialBtn = _doc.rootVisualElement.Q<Button>("main_ui_tutoria_chapter-btn");
        _tutorialBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            _audioSource.clip = _hoverSound;
            _audioSource.Play();
        });
        _tutorialBtn.RegisterCallback<ClickEvent>(evt =>
        {
            _audioSource.clip = _clickSound;
            _audioSource.Play();

            _doc.rootVisualElement.Q<VisualElement>("ui_contain-box").AddToClassList("on");
            ChapterChoiceEvent?.Invoke();
            GameModeManager.Instance.CurrentChapterLevel = ChapterLevel.Tutorial;
            SceneControlManager.FadeOut(() => SceneManager.LoadScene(1));

        });

        _chapterLevelOneBtn = _doc.rootVisualElement.Q<Button>("main_ui_chapter_level_one-btn");
        _chapterLevelOneBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            _audioSource.clip = _hoverSound;
            _audioSource.Play();
        });
        _chapterLevelOneBtn.RegisterCallback<ClickEvent>(evt =>
        {
            _audioSource.clip = _clickSound;
            _audioSource.Play();
            _doc.rootVisualElement.Q<VisualElement>("ui_contain-box").AddToClassList("on");
            ChapterChoiceEvent?.Invoke();
            GameModeManager.Instance.CurrentChapterLevel = ChapterLevel.Easy;
            SceneControlManager.FadeOut(() => SceneManager.LoadScene(1));
        });

        _chapterArrBtn = _doc.rootVisualElement.Q<Button>("main_ui_chapter_arr-btn");
        _chapterArrBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            _audioSource.clip = _hoverSound;
            _audioSource.Play();
        });
        _chapterArrBtn.RegisterCallback<ClickEvent>(evt =>
        {
            _audioSource.clip = _clickSound;
            _audioSource.Play();
            var tabBarContain = _tabBarRoot.Q<VisualElement>($"main_ui_chapters_btn-box");
            foreach (TabBarType tabBarType2 in Enum.GetValues(typeof(TabBarType)))
            {
                var outlineTabBar = _tabBarRoot.Q<VisualElement>($"main_ui_{tabBarType2.ToString().ToLower()}_btn-box");
                outlineTabBar.style.unityBackgroundImageTintColor = _unChoiceColor; //��� �÷� �ʱ�ȭ
            }
            tabBarContain.style.unityBackgroundImageTintColor = _choiceColor;
            _mainUIRoot.style.left = -1920 * 1;
        });

        _settingBtn = _doc.rootVisualElement.Q<Button>("main_ui_setting-btn");
        _settingBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            _audioSource.clip = _hoverSound;
            _audioSource.Play();
        });
        _settingBtn.RegisterCallback<ClickEvent>(evt =>
        {
            _audioSource.clip = _clickSound;
            _audioSource.Play();
            var tabBarContain = _tabBarRoot.Q<VisualElement>($"main_ui_setting_btn-box");
            foreach (TabBarType tabBarType2 in Enum.GetValues(typeof(TabBarType)))
            {
                var outlineTabBar = _tabBarRoot.Q<VisualElement>($"main_ui_{tabBarType2.ToString().ToLower()}_btn-box");
                outlineTabBar.style.unityBackgroundImageTintColor = _unChoiceColor; //��� �÷� �ʱ�ȭ
            }
            tabBarContain.style.unityBackgroundImageTintColor = _choiceColor;
            _mainUIRoot.style.left = -1920 * 2;
        });

        DictionaryInit();
    }
    private void DictionaryInit()
    {
        foreach (TabBarType tabBarType in Enum.GetValues(typeof(TabBarType)))
        {
            string typeName = tabBarType.ToString().ToLower(); //�ҹ��ڷ� ����
            string buttonName = $"main_ui_{typeName}_tabbar-btn"; //����� ������Ʈ �̸� ������
            var tabBarContain = _tabBarRoot.Q<VisualElement>($"main_ui_{typeName}_btn-box"); //����� ������Ʈ ��������
            tabBarContain.RegisterCallback<MouseEnterEvent>(evt =>
            {
                _audioSource.clip = _hoverSound;
                _audioSource.Play();
            });
            Button button = _tabBarRoot.Q<Button>(buttonName); //��ư �ؽ�

            _tabBarDictionary.Add(tabBarType, button); //��ųʸ��� �̳��� Ű������ ��ư �߰�
            _tabBarDictionary[tabBarType].RegisterCallback<ClickEvent>(evt =>
            {
                _audioSource.clip = _clickSound;
                _audioSource.Play();

            });
            _tabBarOutlineDictionary.Add(_tabBarDictionary[tabBarType], tabBarContain);

            _tabBarDictionary[tabBarType].RegisterCallback<ClickEvent>(evt =>
            {
                foreach (TabBarType tabBarType2 in Enum.GetValues(typeof(TabBarType)))
                {
                    var outlineTabBar = _tabBarRoot.Q<VisualElement>($"main_ui_{tabBarType2.ToString().ToLower()}_btn-box");
                    outlineTabBar.style.unityBackgroundImageTintColor = _unChoiceColor; //��� �÷� �ʱ�ȭ
                }
                tabBarContain.style.unityBackgroundImageTintColor = _choiceColor; //������ ��ư�� �÷� ����
                _mainUIRoot.style.left = -1920 * (int)tabBarType;
            });
        }
        _tabBarOutlineDictionary[_tabBarDictionary[TabBarType.Home]].style.unityBackgroundImageTintColor = _choiceColor;
    }

}
