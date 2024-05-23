using UnityEngine;

public class PlayerState
{
    protected Player _owner;
    protected StateMachine _stateMachine;
    protected bool _triggerCall;
    public PlayerState(Player owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
 
    }

    public virtual void Enter()
    {

        _triggerCall = false;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCall = true;
    }
}
