using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhase : PhaseStrategy
{
    private void OnEnable()
    {
        Debug.Log("Hi! I'm the first phase.");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        Debug.Log("waiting...");
        yield return new WaitForSeconds(2);
        FinishPhase();
        Debug.Log("Finished the phase");
    }
}
