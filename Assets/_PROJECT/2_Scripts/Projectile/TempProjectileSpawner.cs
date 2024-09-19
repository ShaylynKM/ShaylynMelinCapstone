using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile _projectile;

    [SerializeField]
    PlayerMovement _playerMovement;
    [SerializeField]
    float _spawnDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            Projectile projectile = Instantiate<Projectile>(_projectile);
            yield return new WaitForFixedUpdate();
            projectile.Initialize(this.transform.position, _playerMovement.gameObject);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
