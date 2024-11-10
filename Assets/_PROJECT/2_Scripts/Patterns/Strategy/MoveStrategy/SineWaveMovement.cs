using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveMovement : MoveStrategy
{
    private Vector3 _initialPosition;
    private Vector3 _perpendicular; // Allows the bullet to move in a sine pattern even at non-zero angles
    private float _startTime;

    [SerializeField] private float _amplitude = 2;
    [SerializeField] private float _frequency = 2;

    public bool Inverted = false;

    private void Start()
    {
        Initialize(_initialPosition, _direction);
    }

    public override void Initialize(Vector3 position, Vector3 direction)
    {
        base.Initialize(position, direction);
        _initialPosition = position;
        _startTime = Time.time;
        _perpendicular = new Vector3(-_direction.y, _direction.x, 0);
        
    }

    protected override void Update()
    {
        float timeSinceSpawn = Time.time - _startTime;
        Vector3 newPos = _initialPosition + _direction * _speed * timeSinceSpawn;
        float offset = Mathf.Sin(timeSinceSpawn * _frequency) * _amplitude;

        if(Inverted == true)
        {
            offset *= -1;
        }

        newPos += _perpendicular * offset;

        transform.position = newPos;
    }
}
