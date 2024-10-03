using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pattern.Singleton;
using UnityEngine.InputSystem;

public class GameInput : Singleton_DontDestroy<GameInput>
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        Interact_Alternate,
        Pause,
    }

    private PlayerInputActions _playerInputActions;


    protected override void Awake()
    {
        base.Awake();

        this._playerInputActions = new PlayerInputActions();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        this._playerInputActions.Player.Enable();

        this._playerInputActions.Player.Interact.performed += Interact_performed;
        this._playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        this._playerInputActions.Player.Pause.performed += Pause_performed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        this._playerInputActions.Player.Disable();

        this._playerInputActions.Player.Interact.performed -= Interact_performed;
        this._playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        this._playerInputActions.Player.Pause.performed -= Pause_performed;

        this._playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementInputNormalize()
    {
        Vector2 inputVector = this._playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return this._playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return this._playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Right:
                return this._playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return this._playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Interact:
                return this._playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Interact_Alternate:
                return this._playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return this._playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            default:
                return null;
        }
    }

    public void Rebinbinding(Binding binding)
    {
        this._playerInputActions.Disable();

        this._playerInputActions.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback =>
        {
            this._playerInputActions.Enable();
        }).Start();

    }

}
