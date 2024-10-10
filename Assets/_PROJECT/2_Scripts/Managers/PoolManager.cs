using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private List<GameObject> _pooledBullets;

    private int _bulletPoolSize = 20;

    public void InitPool(GameObject bulletPrefab) // Initialize the pool
    {
        _pooledBullets = new List<GameObject>();

        GameObject obj;

        for (int i = 0; i < _bulletPoolSize; i++)
        {
            obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            _pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledBullet() // Get a bullet from the pool
    {
        for (int i = 0; i < _bulletPoolSize; i++)
        {
            if (!_pooledBullets[i].activeInHierarchy)
            {
                return _pooledBullets[i];
            }
        }

        return null;
    }

    public void DeactivatePooledBullet(GameObject bullet, Vector3 startPosition)
    {
        bullet.SetActive(false);
        bullet.transform.position = startPosition; // The start position should be the transform of the spawner
    }
}
