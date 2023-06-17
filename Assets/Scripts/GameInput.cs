using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInput PlayerInput;

    private void Awake()
    {
        Instance = this;

        PlayerInput = new PlayerInput();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
