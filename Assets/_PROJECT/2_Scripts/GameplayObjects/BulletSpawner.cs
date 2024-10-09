using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab; // Bullets to be used with this spawner

    [SerializeField]
    private GameObject _target; // Used if the bullet has a specific target to move towards

    // Used to configure the direction
    [SerializeField]
    private float _xDirection = 1f;
    [SerializeField]
    private float _yDirection = 0f;

    // Should replace my individual instantiations with a pool of bullets

    private void SpawnBulletWithDirection()
    {
        GameObject spawnedBullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        if(moveStrategy != null)
        {
            Vector3 direction = new Vector3(_xDirection, _yDirection, 0f);
            moveStrategy.Initialize(this.transform.position, direction); // Initialize the bullet at this object's transform + facing the specified direction
        }
    }

    private void SpawnBulletWithTarget()
    {
        GameObject spawnedBullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);

        MoveStrategy moveStrategy = spawnedBullet.GetComponent<MoveStrategy>();

        if (moveStrategy != null)
        {
            moveStrategy.Initialize(this.transform.position, _target); // Initialize the bullet at this object's transform + facing the target object
        }

    }
}
