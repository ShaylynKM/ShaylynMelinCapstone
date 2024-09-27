using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
    }
}
