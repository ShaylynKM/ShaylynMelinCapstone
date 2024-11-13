using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineSpawner : Spawner
{
    /// <summary>
    /// This is a spawner that moves in a sine wave pattern.
    /// </summary>
    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<SineWaveMovement>();
        _poolObject = _spawnedObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }
}
