using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Vector3 Direction { get; private set; }

    [SerializeField]
    private GameObject _bulletPrefab; // Bullets to be used with this spawner

    private MoveStrategy _moveStrategy; // Used for assigning a direction

    private void Awake()
    {
        _moveStrategy = _bulletPrefab.GetComponent<MoveStrategy>();
    }

    private void SetBulletDirection(Vector3 direction)
    {
        
    }
}
