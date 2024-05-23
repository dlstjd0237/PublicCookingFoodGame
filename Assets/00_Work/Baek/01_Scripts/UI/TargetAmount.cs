using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TargetAmount : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        HandleMoney();
    }
    private void HandleMoney()
    {
        _text.SetText($"¸ñÇ¥Ä¡ $1000/ ${GameModeManager.Instance.Money}");
    }





}
