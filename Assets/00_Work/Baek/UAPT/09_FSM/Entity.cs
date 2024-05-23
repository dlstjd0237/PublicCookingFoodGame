using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    public Animator AnimatorCompo;
    [SerializeField]
    protected StateMachine StateMachineCompo;

    private void Awake()
    {
        StateMachineCompo = gameObject.GetComponent<StateMachine>();
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
    }
}
