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

    private int _basicDamageAmount = 1;

    protected override void Start()
    {
        base.Start();
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);
    }

    public void SnapToCenter()
    {
        transform.position = new Vector3(0, 0, 0); // Move this object to the center
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
    }
}
