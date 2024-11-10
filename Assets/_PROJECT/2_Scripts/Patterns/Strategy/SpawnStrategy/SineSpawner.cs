using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineSpawner : Spawner
{
    private SineWaveMovement _objMoveStrat;

    [SerializeField] private bool _invertSine = false;

    public override void Start()
    {
        base.Start();
        _moveStrategy = GetComponent<NoMoveStrategy>();
        _poolObject = _spawnObject.GetComponent<PoolObject>();
        PoolManager.Instance.InitPool(_poolObject, _poolSize);
    }

    public override void SpawnObject()
    {
        // Had to duplicate this code in order to access the specific instance we're spawning :( If you see this and there's a better way to deal with it please let me know

        PoolObject obj = PoolManager.Instance.Spawn(_poolObject); // Spawns the object with the pool manager

        obj.transform.position = this.transform.position; // Position is on the spawner

        Vector3 direction = new Vector3(Mathf.Cos(_spawnedObjectAngle * Mathf.Deg2Rad), Mathf.Sin(_spawnedObjectAngle * Mathf.Deg2Rad), 0);

        SineWaveMovement objMoveStrategy = obj.GetComponent<SineWaveMovement>();

        if (objMoveStrategy != null)
        {
            objMoveStrategy.Initialize(this.transform.position, direction); // Set the angle of the spawned object
            objMoveStrategy.Inverted = _invertSine;
        }
    }
}
