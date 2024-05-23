using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    protected Player _player;
    Vector3 _playerVelocity = Vector3.zero;

    
    public PlayerGroundState(Player owner, StateMachine stateMachine) : base(owner, stateMachine)
    {
        _player = owner as Player;
    }

    public override void Enter()
    {
        base.Enter();
        _player.Input.CrouchKeyDownEvent += HandleCrouch;
    }

    public override void Exit()
    {
        _player.Input.CrouchKeyDownEvent -= HandleCrouch;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        SetGravity();
        //if(_player.Input.)
    }

    private void SetGravity()
    {
        _playerVelocity.y += _player.Gravity * Time.deltaTime;
        if (_player.IsGround && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;

        _player.CC.Move(_playerVelocity * Time.deltaTime);
    }


    private void HandleCrouch()
    {
        _stateMachine.ChangeState(_player.PlayerCrouchState);
    }
}
