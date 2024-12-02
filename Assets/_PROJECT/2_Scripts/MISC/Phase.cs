using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Phase : MonoBehaviour
{
    /// <summary>
    /// This goes on a phase prefab (which contains all the pattern objects needed)
    /// </summary>

    [SerializeField] private Interval[] _patternPrefabs; // The bullet patterns needed for this phase. Could just be one

    [SerializeField] private float _timeToWait; // How long before the phase is officially considered over

    //private bool _phaseHasFinished = false;

    //public bool PhaseHasFinished { get { return _phaseHasFinished; } }
    public UnityEvent PhaseOver;
    private int _currentPhase = 0;

    private void Start()
    {      
        for (int i = 0; i < _patternPrefabs.Length - 1; i++)        
        {
            _patternPrefabs[i].gameObject.SetActive(false);
            _patternPrefabs[i].Complete += () => BeginPhase(i + 1);
        }
        //_patternPrefabs[_patternPrefabs.Length - 1].Complete += () => PhaseOver?.Invoke();

        _patternPrefabs[_patternPrefabs.Length - 1].Complete += () => Invoke("PhaseOverInvoker", _timeToWait);
    }

    private void PhaseOverInvoker()
    {
        PhaseOver?.Invoke();
    }

    public void BeginPhase(int phaseNumber)
    {
        _patternPrefabs[phaseNumber].gameObject.SetActive(true);
        _patternPrefabs[phaseNumber].Begin();
    }
    // Call this function to set a pattern prefab as active during a phase
    //public void ActivatePatternPrefab()
    //{
    //    for(int i = 0; i < _patternPrefabs.Length; i++)
    //    {
    //        if (_patternPrefabs[i] != null && !_patternPrefabs[i].activeInHierarchy) // if we have objects in the array, and the current one we're checking is inactive
    //        {
    //            _patternPrefabs[i].SetActive(true); // Set this one as active

    //            if(i < _patternPrefabs.Length - 1)
    //            {
    //                _phaseHasFinished = true; // This phase is over when we've reached the end of the array
    //            }
    //            break;
    //        }
    //        else if (_patternPrefabs[i] != null && _patternPrefabs[i].activeInHierarchy)
    //        {
    //            Debug.Log("Phase array element " + i + "is already active");
    //            return;
    //        }

    //        else if (_patternPrefabs[i] = null)
    //        {
    //            Debug.LogError("Nothing in the pattern prefabs array.");
    //        }
    //    }
    //}
}
