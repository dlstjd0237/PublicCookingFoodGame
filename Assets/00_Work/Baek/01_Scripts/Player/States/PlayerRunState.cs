using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(Player owner, StateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        HandleMoveEvent(_player.Input.KeybordDir);

        if (Mathf.Abs(_player.Input.KeybordDir.magnitude) < 0.05f)
        {
            _stateMachine.ChangeState(_player.PlayerIdleState);
        }
    }
    private void HandleMoveEvent(Vector2 dir)
    {
        Vector3 MoveDir = new Vector3(dir.x, 0, dir.y);
        float speed = _player.Speed;
        Vector3 motion = _player.transform.TransformDirection(MoveDir) * Time.deltaTime * speed;
        _player.CC.Move(motion);
    }

}
