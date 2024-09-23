using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MyInputActions _input;

    private Vector2 _moveVector = Vector2.zero;
    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    private float _moveSpeed = 10f;

    [SerializeField]
    private bool _inBattle = false; // If we are in a battle or not

    private bool _playerCanMove = true;

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

        _input.Player.Move.performed += OnMovement;

        _input.Player.Move.canceled += OnMovementCanceled;
    }
    private void OnDisable()
    {
        // Unsubscribe from our input action

        _input.Disable();

        _input.Player.Move.performed -= OnMovement;

        _input.Player.Move.canceled -= OnMovementCanceled;
    }

    private void OnMovement(InputAction.CallbackContext value) // This is what is providing the value to input
    {
        // Allow the player to move when movement is allowed.
        if(_playerCanMove == true)
        {
            _moveVector = value.ReadValue<Vector2>();
        }
        else if(_playerCanMove == false)
        {
            _moveVector = Vector2.zero;
        }
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        _moveVector = Vector2.zero; // Stop the player from moving
    }

    public void EnableMovement()
    {
        // For events
        _playerCanMove = true;
    }
    public void DisableMovement()
    {
        _playerCanMove = false;
    }

    private void FixedUpdate()
    {
        // Using FixedUpdate for physics interactions

        _rb.velocity = _moveVector * _moveSpeed; // move player by multiplying the input by the speed
    }

    private void Update()
    {
        if(_inBattle == false)
        {
            // Flip the sprite when turning left and right
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
}
