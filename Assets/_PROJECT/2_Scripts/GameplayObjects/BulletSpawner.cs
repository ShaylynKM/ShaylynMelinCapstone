using System.Collections.Generic;
using UnityEngine;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab; // Bullets to be used with this spawner

    [SerializeField]
    private GameObject _target; // Used if the bullet has a specific target to move towards

    // Used to configure the direction
    [SerializeField]
    private float _xDirection = 1f;
    [SerializeField]
    private float _yDirection = 0f;

    private void Start()
    {
        PoolManager.Instance.InitPool(_bulletPrefab); // Initialize the pool for this spawner
    }

    private void SpawnBulletWithDirection()
    {
        GameObject spawnedBullet = PoolManager.Instance.GetPooledBullet();

        if(spawnedBullet == null)
        {
            return; // If there are no more bullets in the pool, return
        }

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        if(moveStrategy != null)
        {
            Vector3 direction = new Vector3(_xDirection, _yDirection, 0f);
            moveStrategy.Initialize(this.transform.position, direction); // Initialize the bullet at this object's transform + facing the specified direction
        }

        spawnedBullet.SetActive(true); // Activate the bullet we just spawned
    }

    private void SpawnBulletWithTarget()
    {
        GameObject spawnedBullet = PoolManager.Instance.GetPooledBullet();

        if (spawnedBullet == null)
        {
            return; // If there are no more bullets in the pool, return
        }

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        if (moveStrategy != null)
        {
            moveStrategy.Initialize(this.transform.position, _target); // Initialize the bullet at this object's transform + facing the target object
        }

        spawnedBullet.SetActive(true); // Activate the bullet we just spawned

    }

    private void DespawnBullet(GameObject bulletToDespawn)
    {
        PoolManager.Instance.DeactivatePooledBullet(bulletToDespawn, this.transform.position); // Despawn passed bullet reference and reset its position to the transform of this spawner.
    }
}
