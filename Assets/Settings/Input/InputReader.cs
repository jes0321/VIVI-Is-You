using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private Controls _controls;
    public event Action<Vector2> OnMovementEvent;
    public event Action OnRollbackEvent;
    public event Action OnRollbackEndEvent;

    public event Action OnTurnEndEvent;
    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }
    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        if (movement.magnitude > 0)
        {
            OnMovementEvent?.Invoke(movement);
        }
        if(context.canceled) OnTurnEndEvent?.Invoke();
    }
    public void OnRollback(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnRollbackEvent?.Invoke();
        }
        if(context.canceled) OnRollbackEndEvent?.Invoke();
    }
}
