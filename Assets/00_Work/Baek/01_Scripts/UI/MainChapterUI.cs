using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainChapterUI : UIBase
{
    private ScrollView _chapterContain;
    private AudioSource _audioSource;
    [SerializeField] private VisualTreeAsset _template;
    [SerializeField] private List<ChapterUISO> _chaoterSO = new List<ChapterUISO>();
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;
    public UnityEvent ChapterChoiceEvent;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ScrollViewOffSet();

        ChapterUISlotSet();
    }

    private void ScrollViewOffSet()
    {
        _chapterContain = _doc.rootVisualElement.Q<ScrollView>("main_ui_chapter_contin-scrollview");
        _chapterContain.RegisterCallback<WheelEvent>((evt) =>
        {
            _chapterContain.scrollOffset = new Vector2(_chapterContain.scrollOffset.x + 800 * evt.delta.y, 0);
            evt.StopPropagation();
        });
    }

    private void ChapterUISlotSet()
    {
        for (int i = 0; i < _chaoterSO.Count; ++i)
        {
            CreateChapter(_chaoterSO[i]);
        }
    }

    private void CreateChapter(ChapterUISO chapterUISO)
    {
        var template = _template.Instantiate().Q<VisualElement>();
        var chapterBtn = template.Q<Button>("main_ui_home_chapter-btn");

        chapterBtn.Q<Label>("best_score-label").text = $"최고 기록 : {GameModeManager.Instance.GetChapterScore(chapterUISO.ChapterLevel)}";
        chapterBtn.Q<Label>("main_ui_chapter_name-label").text = chapterUISO.ChapterName;
        chapterBtn.Q<VisualElement>("main_ui_home_chapter_image-box").style.backgroundImage = chapterUISO.ChapterImage.texture;
        chapterBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            _audioSource.clip = _hoverSound;
            _audioSource.Play();
        });
        chapterBtn.RegisterCallback<ClickEvent>(evt =>
        {
            _audioSource.clip = _clickSound;
            _audioSource.Play();
            _doc.rootVisualElement.Q<VisualElement>("ui_contain-box").AddToClassList("on");
            ChapterChoiceEvent?.Invoke();
            GameModeManager.Instance.CurrentChapterLevel = chapterUISO.ChapterLevel;
            SceneControlManager.FadeOut(() => SceneManager.LoadScene(1));
        });
        _chapterContain.Add(template);
    }
}
