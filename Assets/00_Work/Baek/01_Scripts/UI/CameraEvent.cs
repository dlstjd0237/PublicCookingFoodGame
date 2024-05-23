using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{
    private readonly int startHash = Animator.StringToHash("IsStart");
    [SerializeField] private TitleUI _titleUI;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void CameraAnimationEnd()
    {
        _titleUI.TitleOn();
    }

    public void ChoiceChapter()
    {
        _animator.SetBool(startHash, true);
    }



}
