using UnityEngine;

[CreateAssetMenu(menuName = "SO/DialogueDataSO")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private string _speakerName; public string SpeakerName { get { return _speakerName; } }
    [TextAreaAttribute] [SerializeField] private string[] _dialogueInfo; public string[] DialogueInfo { get { return _dialogueInfo; } }
    [SerializeField] private AudioClip _speakSoundClip; public AudioClip SpeakSoundClip { get { return _speakSoundClip; } }
}
