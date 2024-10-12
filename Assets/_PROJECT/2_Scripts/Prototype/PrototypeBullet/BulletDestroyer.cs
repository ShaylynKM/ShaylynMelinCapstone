using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class BulletDestroyer : MonoBehaviour
{
    // Component to tell the bullet if it should destroy itself

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static; // Make sure the colliders have static rigid bodies so they aren't affected by gravity
    }
}
