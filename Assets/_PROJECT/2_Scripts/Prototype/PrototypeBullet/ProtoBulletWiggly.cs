using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBulletWiggly : ProtoBullet
{
    private float _wiggleHeight = 0.02f;

    private float _wiggleSpeed = 2f;

    protected override void Awake()
    {
        this._speed = 6f;
        this._isTrigger = true;

        base.Awake();
    }

    protected override void Update()
    {
        Vector3 bulletPropulsion = new Vector3(this._direction.x * _speed, this._direction.y * _speed, 0f) * Time.deltaTime; // Horizontal movement

        float wiggleStagger = Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleHeight; // Vertical movement

        transform.position += bulletPropulsion + new Vector3(0f, wiggleStagger, 0f); // Add them together
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBattleController>() || (collision.gameObject.GetComponent<BulletDestroyer>()))
        {
            DestroySelf();
        }
    }
}
