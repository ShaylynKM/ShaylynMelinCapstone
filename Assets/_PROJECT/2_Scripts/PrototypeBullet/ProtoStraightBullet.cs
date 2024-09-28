using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoStraightBullet : ProtoBullet
{
    protected override void Awake()
    {
        this._speed = 5f;
        this._isTrigger = true;

        base.Awake();
    }

    // Redundant right now but can be elaborated on later
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroySelf();
        if(collision.gameObject.GetComponent<PlayerBattleController>())
        {
            DestroySelf();
        }
        else if(collision.gameObject.GetComponent<BulletDestroyer>())
        {
            DestroySelf();
        }
    }
}
