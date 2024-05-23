using UnityEngine;

public class NpcExitState : NpcState
{
    private NpcManager _npcManager;
    public NpcExitState(Npc npc, NpcStateMachine npcStateMachine, string boolName) : base(npc, npcStateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _npcManager = FindAnyObjectByType<NpcManager>();
        _npc.NavMeshAgentCompo.SetDestination(_npcManager.ExitTrm.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float distance = Vector3.Distance(_npc.transform.position, _npcManager.ExitTrm.position);
        if (distance < 2f)
        {
            _npc.gameObject.SetActive(false);
            _npc.VisualInit();
            _npcManager.AddList(_npc.sitIndex);
        }
    }
}
