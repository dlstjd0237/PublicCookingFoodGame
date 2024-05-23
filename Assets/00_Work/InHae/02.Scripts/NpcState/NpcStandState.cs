using UnityEngine;

public class NpcStandState : NpcState
{
    public NpcStandState(Npc npc, NpcStateMachine npcStateMachine, string boolName) : base(npc, npcStateMachine, boolName)
    {
    }

    public override void UpdateState()
    {
        if (_endTriggerCalled)
        {
            _npc.transform.position = _npc.placeTrm.Find("Chair/ExitPos").position;
            _npc.StandSetCompo();
            _stateMachine.ChangeState(NpcStateEnum.Exit);
        }
    }
}
