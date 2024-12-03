//using UnityEngine;
//using UnityEngine.Events;

//public class PhaseManager : Singleton<PhaseManager>
//{
//    [SerializeField]
//    private PhasePair[] _phases; // Array of all the phases during a battle

//    private int _phaseIndex;
//    private Phase _currentPhase;
//    private UnityEvent AllPhasesComplete;
//    private void Start()
//    {
//        _phaseIndex = 0; // We are currently on the first phase object

//        _currentPhase = Instantiate(_phases[_phaseIndex].phase); // Activate the first phase object
//        _currentPhase.PhaseFinished += NextPhase;
//        _currentPhase.StartPhase(_phases[_phaseIndex].spawner);
        
//    }

//    public void NextPhase()
//    {
//        _phaseIndex++; // Increment the index
//        if(_phaseIndex >= _phases.Length ) // If we are on the last phase
//        {
//            AllPhasesComplete?.Invoke(); // invoke the event to end the battle

//            return; // and return. Otherwise,
//        }

 
        
//        _currentPhase = Instantiate(_phases[_phaseIndex].phase); // Activate the first phase object
//        _currentPhase.PhaseFinished += NextPhase;
//        _currentPhase.StartPhase(_phases[_phaseIndex].spawner);

//    }
//    [System.Serializable]
//    public struct PhasePair
//    {
//        public Phase phase;
//        public BulletSpawner spawner;
//    }
//    // [System.Serializable]
//    // public struct PhaseInfo
//    // {
//    //     public PhaseStrategy Phase;
//    //     public GameObject PhaseObject;
//    //     public UnityEvent OnBeginPhase; // Event to start a new phase
//    //     public UnityEvent OnEndBattle; // Event to end the battle
//    // }
//}
