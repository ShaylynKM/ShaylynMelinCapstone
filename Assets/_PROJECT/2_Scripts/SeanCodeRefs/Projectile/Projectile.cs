using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    protected MovementStrategy _movementStrategy;
    protected AttackStrategy _attackStrategy;
    // Start is called before the first frame update
    void Awake()
    {
        _movementStrategy = GetComponent<MovementStrategy>();
        _attackStrategy = GetComponent<AttackStrategy>();
    }
    public virtual void Initialize(Vector3 position, GameObject target)
    {
        _movementStrategy.Initialize(position, target);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
