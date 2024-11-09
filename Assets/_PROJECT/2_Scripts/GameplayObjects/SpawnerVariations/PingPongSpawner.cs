using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongSpawner : Spawner
{
    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<PingPongMovement>();
        _poolObject = _spawnObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }
}
