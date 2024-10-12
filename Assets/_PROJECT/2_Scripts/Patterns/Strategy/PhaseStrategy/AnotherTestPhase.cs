using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTestPhase : PhaseStrategy
{
    private void OnEnable()
    {
        Debug.Log("I'm the second phase.");
    }
}
