using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _PROJECT._2_Scripts.Intervals
{
    /// <summary>
    /// We will have discrete intervals at certain times, and , on start, we will execute commands
    /// </summary>
    public class DiscreteInterval : Interval
    {
        [SerializeField] IntervalPair[] intervalPairs;
        private IntervalPair _currentPair;
        [SerializeField] private float timeInterval;

        public override void Begin()
        {
            base.Begin();
            StartCoroutine(RunInterval());
        }

        public override void Stop()
        {
            base.Stop();
            StopAllCoroutines();
        }

        private IEnumerator RunInterval()
        {
            for (int i = 0; i < intervalPairs.Length; i++)
            {
                _currentPair = intervalPairs[i];
                intervalPairs[i].Command.Invoke();
                yield return new WaitForSeconds(intervalPairs[i].time);

            }
            base.Completed();
        }
    }
    [Serializable]
    public struct IntervalPair //each value in interval pair is how long after the previous interval
    {
        public float time;
        public UnityEvent Command;
    }
}