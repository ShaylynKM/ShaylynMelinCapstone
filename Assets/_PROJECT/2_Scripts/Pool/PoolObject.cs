using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public void OnDeSpawn()
    {
        PoolManager.Instance.Despawn(this); // Call the pool manager to despawn this object.
    }
}
