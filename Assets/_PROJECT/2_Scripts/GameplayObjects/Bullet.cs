using UnityEngine;

public class Bullet : PoolObject
{


    public override void OnDespawn()
    {
        PoolManager.Instance.Despawn(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBattleController>() || collision.GetComponent<BulletDestroyer>())
            OnDespawn();
    }
}
