using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance {get; private set;}

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;


    public enum Binding
    {
        Move_up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }

    private PlayerInput PlayerInput;

    private void Awake()
    {
        Instance = this;

        PlayerInput = new PlayerInput();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            PlayerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        PlayerInput.Player.Enable();

        PlayerInput.Player.Interact.performed += Interact_perfomed;
        PlayerInput.Player.InteractAlternate.performed += InteractAlternate_perfomed;
        PlayerInput.Player.Pause.performed += Pause_perfomed;
    }

    private void OnDestroy()
    {
        PlayerInput.Player.Interact.performed -= Interact_perfomed;
        PlayerInput.Player.InteractAlternate.performed -= InteractAlternate_perfomed;
        PlayerInput.Player.Pause.performed -= Pause_perfomed;

        PlayerInput.Dispose();
    }

    private void Pause_perfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_perfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_perfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = PlayerInput.Player.Move.ReadValue<Vector2>();
   
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Move_up:
                return PlayerInput.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return PlayerInput.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return PlayerInput.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return PlayerInput.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return PlayerInput.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return PlayerInput.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return PlayerInput.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        PlayerInput.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_up:
                inputAction = PlayerInput.Player.Move;
                bindingIndex = 1;
            break;
            case Binding.Move_Down:
                inputAction = PlayerInput.Player.Move;
                bindingIndex = 2;
            break;
            case Binding.Move_Left:
                inputAction = PlayerInput.Player.Move;
                bindingIndex = 3;
            break;
            case Binding.Move_Right:
                inputAction = PlayerInput.Player.Move;
                bindingIndex = 4;
            break;
            case Binding.Interact:
                inputAction = PlayerInput.Player.Interact;
                bindingIndex = 0;
            break;
            case Binding.InteractAlternate:
                inputAction = PlayerInput.Player.InteractAlternate;
                bindingIndex = 0;
            break;
            case Binding.Pause:
                inputAction = PlayerInput.Player.Pause;
                bindingIndex = 0;
            break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => { 
            callback.Dispose();
            PlayerInput.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, PlayerInput.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
