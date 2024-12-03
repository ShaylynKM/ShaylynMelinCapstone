using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMovement : MoveStrategy
{
    private GameObject _targetToFollow; // The object this object is following
    private float _rotationSpeed = 5; // How fast this object rotates towards its target

    public GameObject TargetToFollow
    {
        get
        {
            return _targetToFollow;
        }
        set
        {
            _targetToFollow = value;
        }
    }

    public override void Initialize(Vector3 position, GameObject target)
    {
        transform.position = position;
        _targetToFollow = target;
    }

    protected override void Update()
    {
        // Follow the player
        if(_targetToFollow != null)
        {
            Vector2 direction = (_targetToFollow.transform.position - this.transform.position).normalized;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            transform.position += (Vector3)direction * _speed * Time.deltaTime;
        }
    }
}
