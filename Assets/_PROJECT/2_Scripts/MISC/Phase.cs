using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    /// <summary>
    /// This goes on a phase prefab (which contains all the pattern objects needed)
    /// </summary>

    [SerializeField] private GameObject[] _patternPrefabs; // The bullet patterns needed for this phase. Could just be one

    private bool _phaseHasFinished = false;

    public bool PhaseHasFinished { get { return _phaseHasFinished; } }

    private void Start()
    {
        foreach(GameObject pattern in _patternPrefabs)
        {
            pattern.SetActive(false);
        }
    }

    // Call this function to set a pattern prefab as active during a phase
    public void ActivatePatternPrefab()
    {
        for(int i = 0; i < _patternPrefabs.Length; i++)
        {
            if (_patternPrefabs[i] != null && !_patternPrefabs[i].activeInHierarchy) // if we have objects in the array, and the current one we're checking is inactive
            {
                _patternPrefabs[i].SetActive(true); // Set this one as active

                if(i < _patternPrefabs.Length - 1)
                {
                    _phaseHasFinished = true; // This phase is over when we've reached the end of the array
                }
                break;
            }
            else if (_patternPrefabs[i] != null && _patternPrefabs[i].activeInHierarchy)
            {
                Debug.Log("Phase array element " + i + "is already active");
                return;
            }

            else if (_patternPrefabs[i] = null)
            {
                Debug.LogError("Nothing in the pattern prefabs array.");
            }
        }
    }
}
