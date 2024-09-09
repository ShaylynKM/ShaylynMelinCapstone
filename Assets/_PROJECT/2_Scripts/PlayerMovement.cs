using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MyInputActions _input;

    private Vector2 _moveVector = Vector2.zero;

    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    private float _moveSpeed = 10f;

    private void Awake()
    {
        _input = new MyInputActions();

        _rb = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        _moveVector = Vector2.zero; // Stop the player from moving
    }

    private void FixedUpdate()
    {
        // Using FixedUpdate for physics interactions

        _rb.velocity = _moveVector * _moveSpeed; // move player by multiplying the input by the speed
    }

    private void Update()
    {
        if (_moveVector.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_moveVector.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
