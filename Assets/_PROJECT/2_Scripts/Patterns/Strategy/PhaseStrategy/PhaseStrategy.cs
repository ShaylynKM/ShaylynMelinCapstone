using UnityEngine;

public abstract class PhaseStrategy : MonoBehaviour
{
    public virtual void StartPhase()
    {
        // Call this function from OnBeginPhase in the phase manager
    }

    public virtual void FinishPhase()
    {
        PhaseManager.Instance.NextPhase(); // Start the next phase
    }
}
