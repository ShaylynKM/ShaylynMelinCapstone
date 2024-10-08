using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveStrategy : MonoBehaviour
{
    [SerializeField]
    protected Vector2 _direction;

    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected BulletSpawner _spawner;

    public virtual void Initialize(Vector3 position, GameObject target)
    {
        // If the bullet has a specific target
        transform.position = position;
        _direction = (target.transform.position - transform.position).normalized;
    }
     
    public virtual void Initialize(Vector3 position, Vector3 direction)
    {
        // If the bullet just needs a position (direction specified by the spawner)
        transform.position = position;
        _direction = direction;
    }

    public virtual void CalculateDirection(Vector3 direction)
    {
        // Act on the vector to decide which direction the object will move
    }

    protected virtual void Update()
    {
        //transform.rotation.eulerAngles = transform.LookAt(_direction);
        transform.position += new Vector3(_direction.x, _direction.y, 0f) * _speed * Time.deltaTime; // Basic movement
    }
}
