using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhaseManager : MonoBehaviour
{
    //public UnityEvent<PlayerInputState> ChangePlayerState;

    public UnityEvent Dialogue1Start;

    private float _dialogueStartDelay = .02f;

    void Start()
    {
        Invoke("PhaseDialogueBegin", _dialogueStartDelay); // Required a delay to make sure all the event listeners are created in time
    }

    void PhaseDialogueBegin()
    {
        Dialogue1Start?.Invoke();
    }

}
