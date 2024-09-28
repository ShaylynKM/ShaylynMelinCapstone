using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackStrategy : MonoBehaviour
{
    private GameObject _target;//this could be something like something that has the interface "IAttackable"; meaning it can be attacked

    protected int _damageAmount; // How much damage this object does

    [SerializeField] private AttackEffectStrategy _strategy;

    public virtual void Initialize(Vector3 position, GameObject target)
    {
        _target = target; //we make this virtual in case we want to do something specific to that target
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
