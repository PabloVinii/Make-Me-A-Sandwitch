using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput PlayerInput;

    private void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Player.Enable();
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
