using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerBattleController : PlayerController
{
    private Scene _currentScene;

    public UnityEvent<int> PlayerHurt;
    public UnityEvent<int> PlayerHeal;

    private SpriteRenderer _sr;

    private int _basicDamageAmount = 1;

    protected override void Start()
    {
        base.Start();
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);
        _sr = GetComponent<SpriteRenderer>();
    }

    public void SnapToCenter()
    {
        //_sr.enabled = false;
        //transform.position = new Vector3(0, 0, 0); // Move this object to the center

        StartCoroutine(MoveToCenter());
    }

    public void OnPlayerDeath()
    {
        _currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScene.name);
    }

    public void OnPlayerDamaged(int damage)
    {
        PlayerHurt?.Invoke(damage); // Called by the health component
    }

    public void OnPlayerHealed(int health)
    {
        PlayerHeal?.Invoke(health); // Called by the health component
    }

    protected override void FixedUpdate()
    {
        {
            // Using FixedUpdate for physics interactions
            // Debug.Log(Time.fixedDeltaTime);
            //Debug.Log(Time.deltaTime);
            if (_moveVector.magnitude > 0.1f)
            {
                _movementVector = Vector2.Lerp(_rb.velocity, _moveVector * _moveSpeed, _movementStats.AccelerationSpeed);

            }
            else
            {
                _movementVector = Vector2.Lerp(_moveVector * _moveSpeed, _rb.velocity, _movementStats.SlowDownSpeed);

            }
            _rb.velocity = _movementVector;// _moveVector * _moveSpeed; // move player by multiplying the input by the speed

        }
    }

    IEnumerator MoveToCenter()
    {

        while(Vector2.Distance(transform.position, Vector2.zero) > 0.01f) // While we aren't at the center
        {
            transform.position = Vector2.Lerp(transform.position, Vector2.zero, _moveSpeed * Time.deltaTime); // Move to the center from wherever we are
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>()) // If this is a bullet
        {
            OnPlayerDamaged(_basicDamageAmount); // Take damage
        }
        if (collision.gameObject.GetComponent<hpDrop>()) // If this is HP
        {
            OnPlayerHealed(_basicDamageAmount); // Heal a damage point
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>()) // If this is a bullet
        {
            OnPlayerDamaged(_basicDamageAmount); // Take damage
        }
        if(collision.gameObject.GetComponent<hpDrop>()) // If this is HP
        {
            OnPlayerHealed(_basicDamageAmount); // Heal a damage point
        }
    }
    public override void EnableMovement()
    {
        // For events
        _playerCanMove = true;
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);
        _sr.enabled = true;
    }
}
