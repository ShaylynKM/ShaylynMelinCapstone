using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProtoPhaseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _endText;

    [SerializeField]
    PlayerBattleController _player;

    public UnityEvent Dialogue1Start;
    public UnityEvent Dialogue2Start;
    public UnityEvent Dialogue3Start;

    private float _dialogueStartDelay = .02f;

    private float _timeBetweenCircles = 1f; // How long between instantiating the circle pattern in phase 3

    [SerializeField]
    private GameObject _phase1Object;

    [SerializeField]
    private GameObject _phase2Object;

    [SerializeField]
    private GameObject _circlePatternPrefab;

    [SerializeField]
    private DialogueManager _dialogueManager;

    private bool _gameDone = false;

    void Start()
    {
        _endText.SetActive(false);

        _phase1Object.SetActive(false);
        _phase2Object.SetActive(false);

      //  Invoke("Phase1DialogueBegin", _dialogueStartDelay); // Required a delay to make sure all the event listeners are created in time
    }

    void Phase1DialogueBegin()
    {
        Dialogue1Start?.Invoke();
        _dialogueManager.OnDialogueEnded.AddListener(Phase1);
    }

    public void Phase1()
    {
        StartCoroutine(Phase1Coroutine());
    }

    public void Phase2()
    {
        StopCoroutine(Phase1Coroutine());
        StartCoroutine(Phase2Coroutine());
    }

    public void Phase3()
    {
        StopCoroutine(Phase2Coroutine());
        StartCoroutine(Phase3Coroutine());
    }

    IEnumerator Phase1Coroutine()
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);

        _phase1Object.SetActive(true);

        yield return new WaitForSeconds(5);

        _phase1Object.SetActive(false);

        yield return new WaitForSeconds(3);

        _dialogueManager.OnDialogueEnded.RemoveListener(Phase1);

        _dialogueManager.OnDialogueEnded.AddListener(Phase2);

        Dialogue2Start.Invoke();
    }

    IEnumerator Phase2Coroutine()
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);

        _phase2Object.SetActive(true);

        yield return new WaitForSeconds(10);

        _phase2Object.SetActive(false);

        yield return new WaitForSeconds(3);

        _dialogueManager.OnDialogueEnded.RemoveListener(Phase2);

        _dialogueManager.OnDialogueEnded.AddListener(Phase3);

        Dialogue3Start.Invoke();
    }

    IEnumerator Phase3Coroutine()
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);

        Transform playerTransform = _player.GetComponent<Transform>();

        int counter = 0;
        int maximumLoops = 10; // How many times the loop can run

        while(counter < maximumLoops)
        {
            Instantiate(_circlePatternPrefab, playerTransform.position, playerTransform.rotation);

            yield return new WaitForSeconds(_timeBetweenCircles);

            counter++;

            if(counter == maximumLoops)
            {
                break;
            }
        }

        _dialogueManager.OnDialogueEnded.RemoveListener(Phase3);

        _endText.SetActive(true);

        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.PlayerMove);

        _gameDone = true;

    }

    private void Update()
    {
        if(_gameDone == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("ProtoBattle");
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("ProtoOverworld");
            }
        }
    }
}
