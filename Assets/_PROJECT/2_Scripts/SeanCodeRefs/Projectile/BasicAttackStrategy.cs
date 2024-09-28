using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicAttackStrategy : AttackStrategy
{
    [SerializeField]
    private int _basicDamage = 1;

    public UnityEvent CollidedWithPlayer;

    protected override void Start()
    {
        _damageAmount = _basicDamage;
    }

    public void PassDamage(int amount)
    {
        amount = _basicDamage;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerBattleController>())
        {
            CollidedWithPlayer?.Invoke();
            Debug.Log("collided");
        }
    }
}
