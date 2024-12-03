using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MoveStrategy
{
    [SerializeField] private GameObject _pointA, _pointB;
    [SerializeField] private float _pingPongSpeed;
    [SerializeField] protected float _pingPongObjectAngle;

    public override void Initialize(Vector3 position, Vector3 direction)
    {
        position = _pointA.transform.position;

        float angle = _pingPongObjectAngle;

        Vector3 localDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0); // Local direction
        Vector3 worldDirection = transform.TransformDirection(localDirection); // World space direction
    }

    protected override void Update()
    {
        // Move the object back and forth
        float time = Mathf.PingPong(Time.time * _pingPongSpeed, 1);
        transform.position = Vector3.Lerp(_pointA.transform.position, _pointB.transform.position, time);
    }
}
