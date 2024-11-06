using System;
using System.Collections;
using _PROJECT._2_Scripts.Intervals;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Runs continuous interval, executing the same command until it is finished
/// </summary>
public class ContinuousInterval : Interval
{
    protected float _currentTime;
    [SerializeField] private int timesToRun = 5;//number of times to run
    [SerializeField] private float timeInterval;
    [SerializeField] private UnityEvent Command;

    public override void Begin() //when we begin we override the start to do whatever the base class does, but also to make sure that we also keep track of when this started
    {
        base.Begin();
        _currentTime = 0f;
        StartCoroutine(RunInterval());

    }

    public override void Stop()
    {
        base.Stop();
        StopAllCoroutines();
    }
    protected override void Execute() //executes whatever our command is based on intervals
    {
        base.Execute();
        Command.Invoke();
    }
    private IEnumerator RunInterval()
    {
        for (int i = 0; i < timesToRun; i++)
        {
            yield return new WaitForSeconds(timeInterval);
            Execute();
        }

        base.Completed();
    }
    protected override void Update()
    {
        base.Update();
        
    }
}
