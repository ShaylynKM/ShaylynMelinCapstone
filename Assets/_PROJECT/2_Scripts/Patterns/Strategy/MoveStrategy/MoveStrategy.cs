using UnityEngine;

public abstract class MoveStrategy : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _direction;

    [SerializeField]
    protected float _speed;

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    public virtual void Initialize(Vector3 position, GameObject target)
    {
        // If the bullet has a specific target

        transform.position = position; // Bullet's position
        _direction = (target.transform.position - transform.position).normalized; // Bullet's direction (moving towards a target)
    }
     
    public virtual void Initialize(Vector3 position, Vector3 direction)
    {
        // If the bullet just needs a position (direction and position specified by the spawner)

        transform.position = position; // Bullet's position
        _direction = direction.normalized; // Bullet's direction
    }

    protected virtual void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime; // Basic movement
    }
}
