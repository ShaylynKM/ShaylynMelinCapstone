using UnityEngine;
using UnityEngine.Events;

public class PhaseManager : Singleton<PhaseManager>
{
    [SerializeField]
    private PhaseInfo[] _phases; // Array of all the phases during a battle

    private int _phaseIndex;

    private void Start()
    {
        _phaseIndex = 0; // We are currently on the first phase object

        _phases[_phaseIndex].PhaseObject.SetActive(true); // Activate the first phase object

        _phases[_phaseIndex].OnBeginPhase.Invoke(); // Invoke the event to start the phase
    }

    public void NextPhase()
    {
        if(_phaseIndex >= _phases.Length - 1) // If we are on the last phase
        {
            _phases[_phaseIndex].OnEndBattle.Invoke(); // invoke the event to end the battle

            return; // and return. Otherwise,
        }

        _phaseIndex++; // Increment the index

        _phases[_phaseIndex].PhaseObject.SetActive(true); // Activate the game object this phase script is attached to

        _phases[_phaseIndex].OnBeginPhase.Invoke(); // Fire the event to begin the next phase

    }

    [System.Serializable]
    public struct PhaseInfo
    {
        public PhaseStrategy Phase;
        public GameObject PhaseObject;
        public UnityEvent OnBeginPhase; // Event to start a new phase
        public UnityEvent OnEndBattle; // Event to end the battle
    }
}
