using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhaseManager : MonoBehaviour
{
    //public UnityEvent<PlayerInputState> ChangePlayerState;

    public UnityEvent Dialogue1Start;
    public UnityEvent Dialogue2Start;

    private float _dialogueStartDelay = .02f;

    [SerializeField]
    private GameObject _phase1Object;

    [SerializeField]
    private DialogueManager _dialogueManager;

    void Start()
    {
        _phase1Object.SetActive(false);

        Invoke("Phase1DialogueBegin", _dialogueStartDelay); // Required a delay to make sure all the event listeners are created in time
    }

    void Phase1DialogueBegin()
    {
        Dialogue1Start?.Invoke();
        _dialogueManager.OnDialogueEnded.AddListener(Phase1);
        Debug.Log("Added the listener");
    }

    public void Phase1()
    {
        StartCoroutine(Phase1Coroutine());
    }

    IEnumerator Phase1Coroutine()
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);

        _phase1Object.SetActive(true);

        yield return new WaitForSeconds(10);

        _phase1Object.SetActive(false);

        yield return new WaitForSeconds(5);

        _dialogueManager.OnDialogueEnded.RemoveListener(Phase1);
        Debug.Log("removed the listener");

        Dialogue2Start.Invoke();
    }

}
