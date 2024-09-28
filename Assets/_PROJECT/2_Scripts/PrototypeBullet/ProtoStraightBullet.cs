using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoStraightBullet : ProtoBulletStrategy
{
    protected override void Awake()
    {
        this._speed = 5f;
        this._isTrigger = true;

        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerBattleController>()) // If this is the player
        {
            this.DestroySelf(); // Destroy the bullet on contact with the player
        }
    }
}
