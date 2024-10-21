using System.Collections.Generic;
using UnityEngine;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private PoolObject _bulletPrefab; // Bullets to be used with this spawner

    [SerializeField]
    private GameObject _target; // Used if the bullet has a specific target to move towards

    [SerializeField]
    private int _poolSize = 20; // How many bullets per pool

    // Used to configure the direction
    [SerializeField]
    private float _xDirection = 1f;
    [SerializeField]
    private float _yDirection = 0f;

    private void Start()
    {
        PoolManager.Instance.InitPool(_bulletPrefab, _poolSize); // Add all the bullets to the pool
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        SpawnBulletWithDirection();
    //    }
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        SpawnBulletWithTarget();
    //    }
    //}

    private void SpawnBulletWithDirection()
    {
        PoolObject spawnedBullet = PoolManager.Instance.Spawn(_bulletPrefab); // Spawn from a pool of the specific bullet prefab referenced on this object

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        Bullet bullet = spawnedBullet.GetComponent<Bullet>();

        bullet.Init(_bulletPrefab); // Set reference so that this bullet can properly be added to the stack dictionary in PoolManager

        if(moveStrategy != null)
        {
            Vector3 direction = new Vector3(_xDirection, _yDirection, 0f);
            moveStrategy.Initialize(this.transform.position, direction); // Initialize the bullet at this object's transform + facing the specified direction
        }
    }

    private void SpawnBulletWithTarget()
    {
        PoolObject spawnedBullet = PoolManager.Instance.Spawn(_bulletPrefab); // Spawn from a pool of the specific bullet prefab referenced on this object

        Bullet bullet = spawnedBullet.GetComponent<Bullet>();

        bullet.Init(_bulletPrefab); // Set reference so that this bullet can properly be added to the stack dictionary in PoolManager

        if (spawnedBullet == null)
        {
            return; // If there are no more bullets in the pool, return
        }

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        if (moveStrategy != null)
        {
            moveStrategy.Initialize(this.transform.position, _target); // Initialize the bullet at this object's transform + facing the target object
        }
    }
}
