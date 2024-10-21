using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerBattleController : PlayerController
{
    private Scene _currentScene;

    public UnityEvent<int> PlayerHit;

    private int _basicDamageAmount = 1;

    protected override void Start()
    {
        base.Start();
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);
    }

    public void OnPlayerDeath()
    {
        _currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScene.name);
    }

    public void OnPlayerDamaged(int damage)
    {
        PlayerHit?.Invoke(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ProtoBullet>()) // If this is a bullet
        {
            OnPlayerDamaged(_basicDamageAmount); // Take damage
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ProtoBullet>()) // If this is a bullet
        {
            OnPlayerDamaged(_basicDamageAmount); // Take damage
        }
    }
}
