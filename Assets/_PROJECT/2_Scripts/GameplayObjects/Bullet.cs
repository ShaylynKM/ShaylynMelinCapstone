using UnityEngine;

public class Bullet : PoolObject
{
    private PoolObject _bulletPrefab; // Our prefab

    public void Init(PoolObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab; // To set the reference in the spawner
    }

    protected override void OnDespawn()
    {
        PoolManager.Instance.Despawn(this, _bulletPrefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBattleController>() || collision.GetComponent<BulletDestroyer>())
            OnDespawn();
    }
}
