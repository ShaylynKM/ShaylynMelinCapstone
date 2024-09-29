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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerBattleController>() || (collision.gameObject.GetComponent<BulletDestroyer>()))
        {
            DestroySelf();
        }
    }
}
