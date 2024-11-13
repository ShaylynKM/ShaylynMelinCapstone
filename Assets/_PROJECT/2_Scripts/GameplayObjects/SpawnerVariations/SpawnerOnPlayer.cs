using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOnPlayer : Spawner
{
    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<MoveWithPlayer>();
        _poolObject = _spawnedObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }
}
