using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : Spawner
{
    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<NoMoveStrategy>();
        _poolObject = _spawnObject.GetComponent<PoolObject>();
        _interval = GetComponent<ContinuousInterval>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }

    public override void SpawnObject()
    {
        PoolObject obj = PoolManager.Instance.Spawn(_poolObject); // Spawns the object with the pool manager

        obj.transform.position = this.transform.position; // Position is on the spawner

        Vector3 direction = new Vector3(Mathf.Cos(_spawnedObjectAngle * Mathf.Deg2Rad), Mathf.Sin(_spawnedObjectAngle * Mathf.Deg2Rad), 0);

        MoveStrategy objMoveStrategy = obj.GetComponent<MoveStrategy>();

        if (objMoveStrategy != null)
            objMoveStrategy.Initialize(this.transform.position, direction); // Set the angle of the spawned object
    }
}
