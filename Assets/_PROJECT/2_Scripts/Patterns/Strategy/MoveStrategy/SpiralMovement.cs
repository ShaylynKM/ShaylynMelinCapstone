using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiralMovement : MoveStrategy
{ 
    private float _initialRadius = 0.1f;
    private float _angleIncrement;
    private float _rateOfGrowth;

    private float _angle;
    private Vector3 _initialPosition;


    public Vector3 InitialPosition
    {
        get
        {
            return _initialPosition;
        }
        set
        {
            _initialPosition = value;
        }
    }

    public float AngleIncrement
    {
        get
        {
            return _angleIncrement;
        }
        set
        {
            _angleIncrement = value;
        }
    }

    public float RateOfGrowth
    {
        get
        {
            return _rateOfGrowth;
        }
        set
        {
            _rateOfGrowth = value;
        }
    }

    protected override void Update()
    {
        _angle += _angleIncrement * _speed * Time.deltaTime; // Calculate the angle
        float radius = _initialRadius + _rateOfGrowth * _angle;

        float x = radius * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(_angle * Mathf.Deg2Rad);

        transform.position = _initialPosition + new Vector3(x, y, 0); // Update the position based on the object's initial position
    }

    public override void Initialize(Vector3 position, Vector3 direction)
    {
        _initialPosition = position; // Initial position should be on the spawner
        _angle = 0f; // Resets the angle
    }
}
