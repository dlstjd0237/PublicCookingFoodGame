using UnityEngine;

[CreateAssetMenu(menuName = "SO/Chapter")]
public class ChapterUISO : ScriptableObject
{
    [SerializeField] private string _chapterName; public string ChapterName { get { return _chapterName; } }
    [SerializeField] private Sprite _chapterImage; public Sprite ChapterImage { get { return _chapterImage; } }
    [SerializeField] private ChapterLevel _chapterLevel; public ChapterLevel ChapterLevel { get { return _chapterLevel; } }

}
