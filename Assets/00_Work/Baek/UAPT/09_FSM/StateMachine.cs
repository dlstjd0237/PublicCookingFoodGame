using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private PlayerState _currentState;
    public PlayerState CurrentState => _currentState;
    private Player _owner;

    void Start()
    {
        _owner = GetComponent<Player>();
        _currentState = new PlayerIdleState(_owner, this);
        _currentState.Enter();
    }

    void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }
}
