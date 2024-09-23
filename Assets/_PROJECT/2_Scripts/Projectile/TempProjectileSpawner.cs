using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TempProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile _projectile;

    [FormerlySerializedAs("_playerMovement")] [SerializeField]
    PlayerController playerController;
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
            projectile.Initialize(this.transform.position, playerController.gameObject);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
