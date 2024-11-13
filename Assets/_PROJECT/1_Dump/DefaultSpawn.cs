using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSpawn : SpawnStrategy
{
    public override void SpawnBullet(Vector3 position, GameObject prefab)
    {
        
    }

    //IEnumerator SpawnNormally(GameObject prefab)
    //{
    //    PoolManager.Instance.Spawn(_spawnedObject);

    //    initialPosition = _spawnLocation;
    //    prefab = _spawnedObject.gameObject;

    //    prefab.transform.position += new Vector3(_objDirection.x * _objSpeed, _objDirection.y * _objSpeed, 0f) * Time.deltaTime; // Move the bullet
    //}
}
