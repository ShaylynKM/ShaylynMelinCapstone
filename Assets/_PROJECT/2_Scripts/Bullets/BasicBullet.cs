using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicBullet : MonoBehaviour
{
    public UnityEvent OnCollideWithPlayer;

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerBattleController>())
        {
            OnCollideWithPlayer?.Invoke();
        }
    }
}
