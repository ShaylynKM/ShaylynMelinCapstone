using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MoveStrategy
{
    [SerializeField] private GameObject _pointA, _pointB;

    protected override void Update()
    {
        // Move the object back and forth
        float time = Mathf.PingPong(Time.time * _speed, 1);
        transform.position = Vector3.Lerp(_pointA.transform.position, _pointB.transform.position, time);
    }
}
