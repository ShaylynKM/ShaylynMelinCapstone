using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Phase : MonoBehaviour
{
    /// <summary>
    /// This goes on a phase prefab (which contains all the pattern objects needed)
    /// </summary>
    /// 

    [SerializeField] private bool _beginAllAtOnce = false;

    [SerializeField] private Interval[] _patternPrefabs; // The bullet patterns needed for this phase. Could just be one

    [Tooltip("The amount of time before a phase is considered over")]
    [SerializeField] private float _timeBeforeEndPhase; // How long before the phase is officially considered over

    [Tooltip("The amount of time before a phase starts spawning objects")]
    [SerializeField] private float _timeBeforeStartPhase;

    public UnityEvent PhaseOver;

    private int _currentPhase = 0; // Keeps track of what phase we're on
    private int _completedPrefabs = 0; // How many of the prefabs in the phase we've run through
    private bool _canStart = false; // Flag to keep the phase from starting before it's properly triggered


    private void Start()
    {      
        for (int i = 0; i < _patternPrefabs.Length; i++)        
        {
            //_patternPrefabs[i].gameObject.SetActive(false);
            _patternPrefabs[i].Complete += () => OnPrefabComplete();
        }
    }

    public void BeginAfterTrigger()
    {
        _canStart = true;
        BeginPhase(0);
    }
    

    private void OnPrefabComplete()
    {
        _completedPrefabs++; // Increment how many of the phase's prefabs we've cycled through

        if(_completedPrefabs < _patternPrefabs.Length)
        {
            BeginPhase(_completedPrefabs);
        }
        else
        {
            Invoke("PhaseOverInvoker", _timeBeforeEndPhase);
        }
    }

    private void BeginAllPatternsAtOnce()
    {
        if(_canStart == false)
        {
            return;
        }

        _completedPrefabs = 0;

        foreach (var pattern in _patternPrefabs)
        {
            if(!pattern.IsActive)
            {
                pattern.gameObject.SetActive(true);
                pattern.Begin();
            }
        }
        StartCoroutine(WaitForPatternsComplete());
    }

    private IEnumerator WaitForPatternsComplete()
    {
        while(!AllPatternsComplete())
        {
            yield return null; // Wait for the patterns to be complete
        }

        yield return new WaitForSeconds(_timeBeforeEndPhase); // Once all the patterns are complete, we can end

        PhaseOverInvoker(); // And the phase is over
    }

    private bool AllPatternsComplete()
    {
        foreach(var pattern in _patternPrefabs)
        {
            if(pattern.IsActive)
            {
                return false;
            }
        }
        return true; // Only return true once all our patterns are active
    }

    private void PhaseOverInvoker()
    {
        PhaseOver?.Invoke();
    }

    public void BeginPhase(int phaseNumber)
    {
        if(_canStart == false)
        {
            return;
        }

        _currentPhase = phaseNumber; // The phase we drop in the inspector for the event

        if(_beginAllAtOnce == true)
        {
            Invoke("BeginAllPatternsAtOnce", _timeBeforeStartPhase); // Begin all the patterns simultaneously if flagged to do so
        }
        else
        {
            StartCoroutine(BeginWithDelay(phaseNumber)); // Otherwise we begin only the current indexed phase
        }
    }

    IEnumerator BeginWithDelay(int phaseNumber)
    {
        yield return new WaitForSeconds(_timeBeforeStartPhase);

        if (!_patternPrefabs[phaseNumber].gameObject.activeSelf) // Don't activate a prefab that's already active
        {
            _patternPrefabs[phaseNumber].gameObject.SetActive(true);
            _patternPrefabs[phaseNumber].Begin();
        }
    }
}
