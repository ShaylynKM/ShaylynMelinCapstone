using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MyInputActions _input = null;
    private Vector2 _moveVector = Vector2.zero;

    private void Awake()
    {
        _input = new MyInputActions();
    }
    private void OnEnable()
    {
        // Subscribe to our input action
        _input.Enable();
        _input.Player.Movement.performed += OnMovement;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }
    private void OnDisable()
    {
        // Unsubscribe from our input action
        _input.Disable();
        _input.Player.Movement.performed -= OnMovement;
        _input.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovement(InputAction.CallbackContext value) // This is what is providing the value to input
    {

    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {

    }
}
