using System.Collections;
using System.Collections.Generic;
using _PROJECT._0_TryingStuffOut;
using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(TryingStuffMoveStrategy), typeof(TryingStuffSpawnStrategy))] 
public class ReallyBasicSpawner : Spawner
{

    private float _spawnWaitTime = .1f;

    private bool _waitingToSpawn = false;

    private TryingStuffMoveStrategy _moveStrategy;
    private TryingStuffSpawnStrategy _spawnStrategy;
    private void Start()
    {
        _waitingToSpawn = false;
        _moveStrategy = GetComponent<TryingStuffMoveStrategy>();
        _spawnStrategy = GetComponent<TryingStuffSpawnStrategy>();
        // StartCoroutine(SpawnWithSinPattern());
    }

    private void Update()
    {
        _moveStrategy.Move(this.gameObject);
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePosition = Input.mousePosition;
            
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));
            //_spawnStrategy.Spawn(mousePosition,spawnObject);
        }
        // if (Input.GetMouseButton(0) && _waitingToSpawn == false)
        // {
        //     _waitingToSpawn = true;
        //     Vector3 mousePosition = Input.mousePosition;
        //
        //     mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));
        //     //StartCoroutine(WaitToSpawn(mousePosition));
        // }
    }

}
