using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class hpDrop : MonoBehaviour
{
    public UnityEvent OnDestroyHP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBattleController>())
        {
            OnDestroyHP?.Invoke();
        }
    }
}
