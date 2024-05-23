using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerCrouchState : PlayerGroundState
{
    public PlayerCrouchState(Player owner, StateMachine stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.Input.CrouchKeyUpEvent += HandleCrouchUp;
        DOTween.To(() => _player.CC.height, t => _player.CC.height = t, 1f, 0.2f);
    }



    public override void Exit()
    {
        DOTween.To(() => _player.CC.height, t => _player.CC.height = t, 2f, 0.2f);
        _player.Input.CrouchKeyUpEvent -= HandleCrouchUp;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Move(_player.Input.KeybordDir);

    }
    private void HandleCrouchUp()
    {
        _stateMachine.ChangeState(_player.PlayerIdleState);
    }

    private void Move(Vector2 dir)
    {
        Vector3 MoveDir = new Vector3(dir.x, 0, dir.y);
        float speed = _player.Speed * 0.5f;
        Vector3 motion = _player.transform.TransformDirection(MoveDir) * Time.deltaTime * speed;
        _player.CC.Move(motion);
    }
}
