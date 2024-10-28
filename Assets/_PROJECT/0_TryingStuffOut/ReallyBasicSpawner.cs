using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReallyBasicSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _circlePrefab;

    private float _spawnWaitTime = .1f;

    private bool _waitingToSpawn = false;

    private void Start()
    {
        _waitingToSpawn = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _waitingToSpawn == false)
        {
            _waitingToSpawn = true;

            StartCoroutine(WaitToSpawn());
        }
    }

    IEnumerator WaitToSpawn()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));

        Instantiate(_circlePrefab, mousePosition, _circlePrefab.transform.rotation);

        yield return new WaitForSeconds(_spawnWaitTime);

        _waitingToSpawn = false;
    }
}
