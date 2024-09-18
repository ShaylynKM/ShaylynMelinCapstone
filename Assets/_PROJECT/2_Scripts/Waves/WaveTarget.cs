using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody))] //require component forces us to take on a component
public class WaveTarget : MonoBehaviour
{
    #region Serialize fields
    [SerializeField]
    float timeAfterDeath = .1f;

    #endregion
    #region Public variables
    [HideInInspector]
    public UnityEvent targetHit; //this is syntax used for events; I know it's kind of gross
    #endregion
    #region private variables
    Rigidbody _rb;
    Vector3 moveVector;
    float speed;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; //this is because i want it to collide with static and dynamic rigid bodies
    }
    public void StartMoving(Vector3 direction, float speedValue)
    {
        moveVector = direction;
        speed = speedValue;
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.GetComponent<Projectile>())
        //{
        //    targetHit?.Invoke();//call the function
        //    Invoke("Death", timeAfterDeath); //destroy the target after timeAfterDeath
        //}
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}