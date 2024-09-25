using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MyInputActions _input;

    private Vector2 _moveVector = Vector2.zero;
    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    private float _moveSpeed;

    [SerializeField]
    private PlayerActionsSO _playerActionsSO;

    [SerializeField]
    private PlayerMovementSO _movementStats;

    private bool _playerCanMove = true;

    private bool _inBattle = false;

    private Vector2 _movementVector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _moveSpeed = _movementStats.MoveSpeed; // Set the speed to what is defined in the scriptable object
    }

    private void Start()
    {
        // Enable was being called before Singleton was created (probably)
        PlayerInputManager.Instance.PlayerMove += HandleMove;
        PlayerInputManager.Instance.PlayerInteract += HandleInteract;

    }

    private void OnEnable()
    {

    }

    private void HandleMove(Vector2 movement)
    {
        //Debug.Log(($"Received movement of {movement}"));
        if(_playerCanMove == true)
        {
            _moveVector = movement;
        }
        else if(_playerCanMove == false)
        {
            _moveVector = Vector2.zero;
        }
    }

    private void HandleInteract()
    {
        _playerActionsSO.HandleInteract();
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
        // Debug.Log(Time.fixedDeltaTime);
        //Debug.Log(Time.deltaTime);
        _movementVector = Vector2.Lerp(_rb.velocity, _moveVector * _moveSpeed, _movementStats.SlowDownSpeed);
        _rb.velocity = _movementVector;// _moveVector * _moveSpeed; // move player by multiplying the input by the speed

    }

    private void Update()
    {
        if (_movementStats.Flip == true)
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
