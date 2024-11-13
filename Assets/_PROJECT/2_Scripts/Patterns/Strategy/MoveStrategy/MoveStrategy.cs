using UnityEngine;

public abstract class MoveStrategy : MonoBehaviour
{
    protected Vector3 _direction;

    protected float _speed;

    protected float _timeBeforeMoving = .5f;
    protected float _timeBeforeDespawn = .5f;

    protected bool _waitingToMove = true;
    protected bool _readyToDespawn = false;

    public bool ReadyToDespawn
    {
        get
        {
            return _readyToDespawn;
        }
    }

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
    public float TimeBeforeMoving
    {
        get
        {
            return _timeBeforeMoving;
        }
        set
        {
            _timeBeforeMoving = value;
        }
    }

    public float TimeBeforeDespawn
    {
        get
        {
            return TimeBeforeDespawn;
        }
        set
        {
            _timeBeforeDespawn = value;
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
