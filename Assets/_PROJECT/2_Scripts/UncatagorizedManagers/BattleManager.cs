using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{ 
    ///  <summary>
    /// This class is responsible for linking the dialogue and battle phases together.
    /// Needs references to the dialogue objects
    /// Needs to be able to instantiate the bullet pattern objects for each phase
    /// Should be able to instantiate more than one pattern object during a phase
    /// Phases should be their own objects. All patterns needed exist inside that object, and they get set as active at specific times or in response to specific conditions (intervals)
    /// This script could have a list of the phase objects to activate??? Deciding if I want them all in the hierarchy at the start or instantiate them at runtime (it would be easier to set up events if they were all present in the scene at once
    /// </summary>

    [SerializeField] private GameObject[] _phases; // Objects for each phase in the entire battle
    //[SerializeField] private GameObject[] _dialogueObjects; // Objects containing the dialogue for each section

    public UnityEvent PhaseFinished;
    public UnityEvent OnBeginScene;

    private void Start()
    {
        foreach (GameObject phase in _phases)
        {
            phase.SetActive(false);
        }
        Invoke("Begin", .1f);
    }
    public void Begin()
    {
        OnBeginScene?.Invoke();
    }

    //public void StartPhase()
    //{
    //    for(int i = 0; i < _phases.Length; i++)
    //    {
    //        if(_phases[i] != null && !_phases[i].activeInHierarchy)
    //        {
    //            _phases[i].SetActive(true); // Set this phase as active

    //            Phase phaseScript = _phases[i].GetComponent<Phase>();

    //            if (phaseScript.PhaseHasFinished == true)
    //            {
    //                PhaseFinished.Invoke(); // Invoke the event for when a phase finishes
    //            }
    //            break;
    //        }
    //        else if (_phases[i] != null && _phases[i].activeInHierarchy)
    //        {
    //            Debug.Log("Phase array element " + i + "is already active");
    //            return;
    //        }

    //        else if (_phases[i] = null)
    //        {
    //            Debug.LogError("Nothing in the phase array.");
    //        }
    //    }
    //}
    public void StartNextPhase()
    {

    }
    public void StartSpecificPhase(Phase newPhase)
    {
        newPhase.gameObject.SetActive(true);
        newPhase.BeginPhase(0);
    }

    public void BattleEnd()
    {
        // do stuff
    }
}
