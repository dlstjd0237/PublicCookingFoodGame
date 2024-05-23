using UnityEngine;

public class NpcEnterState : NpcState
{
    private Vector3 _compareTrm;
    private Transform _chairTrm;
    private NpcManager _npcManager;
    public NpcEnterState(Npc npc, NpcStateMachine npcStateMachine, string boolName) : base(npc, npcStateMachine, boolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _npcManager = FindAnyObjectByType<NpcManager>();
        _npc.RandomFoodPick();
        _npc.sitIndex = _npcManager.RandomNumPick();
        _npc.placeTrm = _npcManager._sitTrmList[_npc.sitIndex];
        _chairTrm = _npc.placeTrm.Find("Chair/SitPos");
    }

    public override void UpdateState()
    {
        _npc.SetMovement(_chairTrm.position);
        float distance = Vector3.Distance(_npc.transform.position, _chairTrm.position);
        if (distance < 2f)
        {
            _stateMachine.ChangeState(NpcStateEnum.Sit);
        }
    }
}
