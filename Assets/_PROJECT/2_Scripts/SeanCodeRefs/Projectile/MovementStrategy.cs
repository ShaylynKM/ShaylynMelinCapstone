using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategy : MonoBehaviour
{
    [SerializeField]
    protected Vector2 _direction;
    [SerializeField]
    protected float _speed = 5f;
    
    public virtual void Initialize(Vector3 position, GameObject target)
    {
        this.transform.position = position;
        this._direction = (target.transform.position - transform.position).normalized;

       // this._direction = direction;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        this.transform.position += new Vector3(this._direction.x * _speed, this._direction.y * _speed, 0f) * Time.deltaTime; 
    }
}
