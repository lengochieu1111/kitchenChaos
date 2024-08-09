using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pattern.Singleton;

public class GameInput : Singleton_DontDestroy<GameInput>
{
    public event EventHandler OnInteractAction;


    private PlayerInputActions _playerInputActions;


    protected override void Awake()
    {
        base.Awake();

        this._playerInputActions = new PlayerInputActions();
        this._playerInputActions.Player.Enable();

        this._playerInputActions.Player.Interact.performed += Interact_performed;

    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementInputNormalize()
    {
        Vector2 inputVector = this._playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
