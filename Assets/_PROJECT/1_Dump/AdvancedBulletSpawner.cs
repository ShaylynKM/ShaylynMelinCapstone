//using System.Collections.Generic;
//using UnityEngine;
//public class AdvancedBulletSpawner : BulletSpawner
//{
    
//    [SerializeField] private Transform[] spawnLocations;
    
//    // Used to configure the direction
    
//    private void Start()
//    {
//        PoolManager.Instance.InitPool(_bulletPrefab, _poolSize); // Add all the bullets to the pool
//    }

//    //private void Update()
//    //{
//    //    if (Input.GetMouseButtonDown(0))
//    //    {
//    //        SpawnBulletWithDirection();
//    //    }
//    //    if(Input.GetMouseButtonDown(1))
//    //    {
//    //        SpawnBulletWithTarget();
//    //    }
//    //}

//    public override void SpawnBulletWithDirection()
//    {
//        foreach (Transform spawnLocation in spawnLocations)
//        {
//            PoolObject spawnedBullet = PoolManager.Instance.Spawn(_bulletPrefab); // Spawn from a pool of the specific bullet prefab referenced on this object

//            MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

//            Bullet bullet = spawnedBullet.GetComponent<Bullet>();

//            bullet.Init(_bulletPrefab); // Set reference so that this bullet can properly be added to the stack dictionary in PoolManager

//            if(moveStrategy != null)
//            {
//                Vector3 direction = new Vector3(_xDirection, _yDirection, 0f);
//                moveStrategy.Initialize(spawnLocation.position, direction); // Initialize the bullet at this object's transform + facing the specified direction
//            }
//        }

//    }

//    public override void SpawnBulletWithTarget()
//    {
//        PoolObject spawnedBullet = PoolManager.Instance.Spawn(_bulletPrefab); // Spawn from a pool of the specific bullet prefab referenced on this object

//        Bullet bullet = spawnedBullet.GetComponent<Bullet>();

//        bullet.Init(_bulletPrefab); // Set reference so that this bullet can properly be added to the stack dictionary in PoolManager

//        if (spawnedBullet == null)
//        {
//            return; // If there are no more bullets in the pool, return
//        }

//        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

//        if (moveStrategy != null)
//        {
//            moveStrategy.Initialize(this.transform.position, _target); // Initialize the bullet at this object's transform + facing the target object
//        }
//    }
//}
