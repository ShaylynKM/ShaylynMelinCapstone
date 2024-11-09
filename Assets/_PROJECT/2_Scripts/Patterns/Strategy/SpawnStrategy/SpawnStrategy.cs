using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStrategy : MonoBehaviour
{
    [SerializeField] protected PoolObject _spawnObject;

    [SerializeField] protected int _poolSize;

    [SerializeField] protected Vector2 _objDirection; // Which direction spawned objects will travel
    [SerializeField] protected float _objSpeed; // How fast spawned objects will travel

    [SerializeField] protected float _objDestroyTime; // How long before spawned objects are destroyed


    protected Vector3 _spawnLocation;

    public virtual void Start()
    {
        PoolManager.Instance.InitPool(_spawnObject, _poolSize);
        _spawnLocation = this.transform.position;
    }

    public virtual void SpawnBullet(Vector3 position, GameObject prefab)
    {

    }
}
