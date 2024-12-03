using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MyInputActions _input;

    protected Vector2 _moveVector = Vector2.zero;
    protected Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    protected float _moveSpeed;

    [SerializeField]
    private PlayerActionsSO _playerActionsSO;

    [SerializeField]
    protected PlayerMovementSO _movementStats;

    [SerializeField]
    private AnimationClip _walkAnimation;

    protected bool _playerCanMove = true;

    protected Vector2 _movementVector;

    public UnityEvent StartWalkAnimation;
    public UnityEvent StopWalkAnimation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _moveSpeed = _movementStats.MoveSpeed; // Set the speed to what is defined in the scriptable object
    }

    protected virtual void Start()
    {
        // Enable was being called before Singleton was created (probably)
        PlayerInputManager.Instance.PlayerMove += HandleMove;
        PlayerInputManager.Instance.PlayerInteract += HandleInteract;
        PlayerInputManager.Instance.PlayerStop += () =>
        {
            _playerCanMove = false;
            _moveVector = new Vector2();
        };

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
    public virtual void EnableMovement()
    {
        // For events
        _playerCanMove = true;
    }
    public void DisableMovement()
    {
        _playerCanMove = false;
    }

    protected virtual void FixedUpdate()
    {
        // Using FixedUpdate for physics interactions
        // Debug.Log(Time.fixedDeltaTime);
        //Debug.Log(Time.deltaTime);
        if(_moveVector.magnitude > 0.1f)
        {
            _movementVector = Vector2.Lerp(_rb.velocity, _moveVector * _moveSpeed, _movementStats.AccelerationSpeed);
            StartWalkAnimation?.Invoke();

        }
        else
        {
            _movementVector = Vector2.Lerp(_moveVector * _moveSpeed, _rb.velocity, _movementStats.SlowDownSpeed);
            StopWalkAnimation.Invoke();

        }
        _rb.velocity = _movementVector;// _moveVector * _moveSpeed; // move player by multiplying the input by the speed

    }

    private void Update()
    {
        if (_movementStats.Flip == true)
        {
            // Flip the sprite when turning left and right
            if (_moveVector.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_moveVector.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

    }
}
