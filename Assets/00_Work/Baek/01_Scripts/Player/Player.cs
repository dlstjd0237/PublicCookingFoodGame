using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : Entity
{
    [Header("PlayerSetting")]
    public float Speed = 4.0f;
    public float Gravity = -9.8f;
    public float Distance = 2.0f;
    public bool IsGround { get; private set; }
    private float _currentTime;


    [field: SerializeField]
    public InputReader Input { get; private set; }
    public CharacterController CC { get; private set; }

    #region State

    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerRunState PlayerRunState { get; private set; }
    public PlayerCrouchState PlayerCrouchState { get; private set; }

    #endregion



    private void Awake()
    {
        _currentTime = 0;

        CC = GetComponent<CharacterController>();

        Input.Console.Floor.Enable();
        Input.Console.UI.Enable();

        PlayerIdleState = new PlayerIdleState(this, StateMachineCompo);
        PlayerRunState = new PlayerRunState(this, StateMachineCompo);
        PlayerCrouchState = new PlayerCrouchState(this, StateMachineCompo);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        IsGround = CC.isGrounded;
 
    }

    private void OnEnable()
    {
        GameModeManager.Instance.HappeyEndingEvent += HandleHappeyEnding;
    }
    private void OnDisable()
    {
        GameModeManager.Instance.HappeyEndingEvent -= HandleHappeyEnding;
    }

    private void HandleHappeyEnding()
    {
        GameModeManager.Instance.SaveTime(_currentTime);
        SceneControlManager.FadeOut(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
