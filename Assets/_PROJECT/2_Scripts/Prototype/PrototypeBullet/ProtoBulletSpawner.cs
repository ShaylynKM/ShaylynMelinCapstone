using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProtoBulletSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet; // The bullet prefab we spawn

    [SerializeField]
    GameObject _target; // Where these bullets should be travelling

    [SerializeField]
    float _spawnDelay = 2f; // Time between spawning bullets

    private void Start()
    {
        StartCoroutine(BulletSpawner());
    }

    IEnumerator BulletSpawner()
    {
        while (true)
        {
            GameObject bulletPrefab = Instantiate(_bullet, this.transform.position, Quaternion.identity); // Store reference to the instantiated bullet

            ProtoBullet bulletScript = bulletPrefab.GetComponent<ProtoBullet>(); // Get the bullet's script

            bulletScript.SetTarget(_target); // Set this bullet's target

            yield return new WaitForFixedUpdate();

            yield return new WaitForSeconds(_spawnDelay); // Wait before spawning a new bullet
        }
    }
}
