using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapMovement : MoveStrategy
{
    private GameObject _target;
 
    public GameObject Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }  

    public override void Initialize(Vector3 position, GameObject target)
    {
        _target = target;

        transform.position = position; // Bullet's position

        _direction = (target.transform.position - transform.position).normalized; // Bullet's direction (moving towards a target)

        _waitingToMove = true;

        StartCoroutine(WaitBeforeMoving());
    }

    protected override void Update()
    {
        if (_waitingToMove == false)
            transform.position += _direction * _speed * Time.deltaTime; // Basic movement
    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(_timeBeforeMoving);
        _waitingToMove = false;
    }

    IEnumerator WaitBeforeDespawning()
    {
        _waitingToMove = true;
        yield return new WaitForSeconds(_timeBeforeDespawn);
        PoolManager.Instance.Despawn(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _target)
        {
            StartCoroutine(WaitBeforeDespawning());
        }
    }
}
