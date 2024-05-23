using System;
using System.Collections;
using UnityEngine;

public class NpcSitState : NpcState
{
    public NpcSitState(Npc npc, NpcStateMachine npcStateMachine, string boolName) : base(npc, npcStateMachine, boolName)
    {
    }
    private readonly int angryHashCode = Animator.StringToHash("Angry");
    private readonly int realAngryHashCode = Animator.StringToHash("RealAngry");

    private bool _oneTimeAngryAnim;
    private bool _oneTimeRealAngryAnim;
    private bool _checkFood;

    private float percent;
    private float deltaTime;

    private MeshRenderer waitBarMeshRenderer;
    private Transform waitBarParent;
    private TableServingCheck _tableServingCheck;
    public override void Enter()
    {
        base.Enter();
        _npc.SitSetCompo(); 
        _checkFood = false;
        _npc.WaitBarTrm.gameObject.SetActive(true);
        waitBarMeshRenderer = _npc.WaitBarTrm.GetComponentInChildren<MeshRenderer>();
        waitBarParent = _npc.WaitBarTrm.Find("WaitBarParent");
        _tableServingCheck = _npc.placeTrm.Find("ServingPos").GetComponent<TableServingCheck>();
    }

    public override void UpdateState()
    {
        deltaTime += Time.deltaTime;
        percent = deltaTime / _npc.waitTime;

        WaitBarDecreases();
        AngryAnimationCheck();
        if (_tableServingCheck.plateFoodType == _npc._npcFood && !_checkFood)
        {
            _tableServingCheck.plate.DestroyFood();
            _checkFood = true;
        }

        if (1f < percent || _checkFood)
        {
            _npc.AnimationCompo.SetBool(realAngryHashCode, false);
            _npc.AnimationCompo.SetBool(angryHashCode, false);
            _npc.WaitBarTrm.gameObject.SetActive(false);
            _stateMachine.ChangeState(NpcStateEnum.Stand);
        }
    }

    private void WaitBarDecreases()
    {
        float value = Mathf.Lerp(1, 0, percent);
        waitBarParent.localScale = new Vector3(value, 1, 1);

        if (percent < 0.5f)
        {
            waitBarMeshRenderer.material.color = Color.Lerp(Color.green, Color.yellow, percent * 2);
        }
        if (0.5f < percent)
        {
            waitBarMeshRenderer.material.color = Color.Lerp(Color.yellow, Color.red, (percent - 0.5f) * 2.2f);
        }
    }

    private void AngryAnimationCheck()
    {
        if (percent >= 0.75f && !_oneTimeRealAngryAnim)
        {
            if (!_oneTimeAngryAnim)
            {
                _npc.AnimationCompo.SetBool(angryHashCode, false);
                _oneTimeAngryAnim = true;
            }

            _npc.AnimationCompo.SetBool(_animBoolHash, false);
            _npc.AnimationCompo.SetBool(realAngryHashCode, true);
            _npc.Visual.localRotation = Quaternion.Euler(Vector3.zero);

            if (_endTriggerCalled)
            {
                _npc.AnimationCompo.SetBool(realAngryHashCode, false);
                _npc.AnimationCompo.SetBool(_animBoolHash, true);
                _oneTimeRealAngryAnim = true;
                _endTriggerCalled = false;
            }
        }
        else if (percent >= 0.5f && !_oneTimeAngryAnim)
        {
            _npc.AnimationCompo.SetBool(_animBoolHash, false);
            _npc.AnimationCompo.SetBool(angryHashCode, true);
            _npc.Visual.localRotation = Quaternion.Euler(Vector3.zero);

            if (_endTriggerCalled)
            {
                _npc.AnimationCompo.SetBool(angryHashCode, false);
                _npc.AnimationCompo.SetBool(_animBoolHash, true);
                _oneTimeAngryAnim = true;
                _endTriggerCalled = false;
            }
        }
    }

    public override void Exit()
    {
        if (!_checkFood)
            UIManager_inhae.Instance.SetHealth(_npc.failDamage);

        deltaTime = 0;
        percent = 0;
        waitBarMeshRenderer.material.color = Color.green;
        waitBarParent.localScale = Vector3.one;

        _oneTimeAngryAnim = false;
        _oneTimeRealAngryAnim = false;
        base.Exit();
    }
}
