using UnityEngine;

public abstract class NpcState : MonoBehaviour
{
    protected NpcStateMachine _stateMachine;
    protected Npc _npc;
    
    protected int _animBoolHash;
    
    protected bool _endTriggerCalled;

    public NpcState(Npc npc, NpcStateMachine npcStateMachine, string boolName)
    {
        _npc = npc;
        _stateMachine = npcStateMachine;
        _animBoolHash = Animator.StringToHash(boolName);
    }
    
    public virtual void Enter()
    {
        _endTriggerCalled = false;
        _npc.AnimationCompo.SetBool(_animBoolHash, true);
    }
    
    public virtual void UpdateState()
    {
    }
    
    public virtual void Exit()
    {
        _npc.AnimationCompo.SetBool(_animBoolHash, false);
    }
    
    public virtual void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
