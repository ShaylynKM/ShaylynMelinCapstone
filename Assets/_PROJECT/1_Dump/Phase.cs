//using System;
//using UnityEngine;

//public abstract class Phase : MonoBehaviour
//{
//    //every phase has a spawner, a start, and an end to the phase
//    //your phase assigns the bullet spawner to the phase on the fly
//    protected BulletSpawner _bulletSpawner;
//    [field: SerializeField] public bool SpawnWithDirection { get; private set; } = true;
//    public event Action PhaseFinished, PhaseBegins;
//    public virtual void StartPhase(BulletSpawner bulletSpawner)
//    {
//        _bulletSpawner = bulletSpawner;
//        PhaseBegins?.Invoke();
//        // Call this function from OnBeginPhase in the phase manager
//    }

//    public virtual void FinishPhase()
//    {
//        PhaseFinished?.Invoke();
//       // PhaseManager.Instance.NextPhase(); // Start the next phase
//    }
//}
