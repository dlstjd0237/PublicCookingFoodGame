using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine
{
    public NpcState CurrentState { get; private set; }
    public Dictionary<NpcStateEnum, NpcState> stateDictionary;

    public NpcStateMachine()
    {
        stateDictionary = new Dictionary<NpcStateEnum, NpcState>();
    }

    public void Init(NpcStateEnum startState)
    {
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(NpcStateEnum newState)
    {
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(NpcStateEnum stateEnum, NpcState state)
    {
        stateDictionary.Add(stateEnum, state);
    }
}
