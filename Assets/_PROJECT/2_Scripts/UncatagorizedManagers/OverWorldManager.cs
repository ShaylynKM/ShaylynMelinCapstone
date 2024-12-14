using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OverWorldManager : MonoBehaviour
{
    public UnityEvent OnStartBattle;

    private void Start()
    {
        StartCoroutine(WaitToStart());
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(5);
        OnStartBattle?.Invoke();
    }
}
