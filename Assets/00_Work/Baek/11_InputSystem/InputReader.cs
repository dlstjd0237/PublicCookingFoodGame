using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReder")]
public class InputReader : ScriptableObject, Console.IFloorActions, Console.IUIActions
{
    public event Action LeftMouseDownEvent;
    public event Action LeftMouseUpEvent;
    public event Action RightMouseDownEvent;
    public event Action RightMouseUpEvent;
    public event Action CrouchKeyUpEvent;
    public event Action CrouchKeyDownEvent;
    public event Action EsckeyEvent;
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> MouseMoveEvent;
    public Vector2 KeybordDir { get; private set; }

    private Console _console;
    public Console Console { get => _console; set => _console = value; }
    private void OnEnable()
    {

        if (_console is null)
        {
            _console = new Console();
            _console.Floor.SetCallbacks(this);
            _console.UI.SetCallbacks(this);
        }
        _console.Enable();
    }


    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            LeftMouseDownEvent?.Invoke();
        }
        if (context.action.WasReleasedThisFrame())
        {
            LeftMouseUpEvent?.Invoke();
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {

        if (context.action.WasPressedThisFrame())
        {
            RightMouseDownEvent?.Invoke();
        }
        if (context.action.WasReleasedThisFrame())
        {
            RightMouseUpEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        KeybordDir = context.ReadValue<Vector2>().normalized;
        MoveEvent?.Invoke(KeybordDir);
    }


    public void OnMousePos(InputAction.CallbackContext context)
    {
        MouseMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            CrouchKeyDownEvent?.Invoke();
        }
        if (context.action.WasReleasedThisFrame())
        {
            CrouchKeyUpEvent?.Invoke();
        }
    }

    public void OnOptione(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EsckeyEvent?.Invoke();
        }
    }
}
