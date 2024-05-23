using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{


    public PlayerIdleState(Player owner, StateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(_player.Input.KeybordDir.magnitude) > 0.05f)
        {
            _stateMachine.ChangeState(_player.PlayerRunState);
        }
    }
}
