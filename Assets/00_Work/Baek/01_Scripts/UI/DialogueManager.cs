using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] private DialogueData so;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _profileImage;
    [SerializeField] private AudioSource _audioSource;

    [Range(0f, 2f)]
    [SerializeField] private float _textSpeed;

    private Coroutine _dialogueCoroutine;

    private void OnEnable()
    {
        StartDialogue(so);
    }

    public void StartDialogue(DialogueData so)
    {
        if (_dialogueCoroutine != null)
            return;
        _canvasGroup.DOFade(1, 1);
        _dialogueCoroutine = StartCoroutine(DialogueCoroutine(so));
    }


    private IEnumerator DialogueCoroutine(DialogueData so)
    {
        _nameText.text = so.SpeakerName;
        for (int i = 0; i < so.DialogueInfo.Length; ++i)
        {
            for (int j = 0; j < so.DialogueInfo[i].Length; ++j)
            {
                _audioSource.clip = so.SpeakSoundClip;
                _audioSource.Play();
                string info = so.DialogueInfo[i];
                _infoText.text += info[j];
                yield return new WaitForSeconds(_textSpeed);
            }
            _audioSource.Stop();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) == true);
            _infoText.text = "";
        }
        _canvasGroup.DOFade(0, 1);
    }
}
