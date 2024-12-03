using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSpawner : Spawner
{
    /// <summary>
    /// This is a spawner that moves in spirals.
    /// </summary>
    /// 

    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<SpiralMovement>();
        _poolObject = _spawnedObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }
}
