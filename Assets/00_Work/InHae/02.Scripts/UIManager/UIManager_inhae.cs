using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager_inhae : MonoSingleton<UIManager_inhae>
{
    [SerializeField] private float maxHealth;
    public Image _healthSlider;
    public GameObject[] _ending;
    [SerializeField] private InputReader _input;
    public void SetHealth(float damage)
    {
        float value = _healthSlider.fillAmount;
        value -= damage / maxHealth;
        if (value <= 0)
        {
            Bad();
        }
        else
        {
            _healthSlider.DOFillAmount(value, 1);
        }
    }

    private void Bad()
    {
        _input.Console.Floor.Disable();
        _input.Console.UI.Disable();
        for (int i = 0; i < _ending.Length; ++i)
        {
            _ending[i].SetActive(true);
        }
    }
}
