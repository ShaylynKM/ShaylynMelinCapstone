using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhase : Phase
{
    public override void StartPhase(BulletSpawner bulletSpawner)
    {
        base.StartPhase(bulletSpawner);
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
