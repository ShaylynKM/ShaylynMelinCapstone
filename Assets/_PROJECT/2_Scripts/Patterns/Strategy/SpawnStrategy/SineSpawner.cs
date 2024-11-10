using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineSpawner : Spawner
{
    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<NoMoveStrategy>();
        _poolObject = _spawnObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }
}
