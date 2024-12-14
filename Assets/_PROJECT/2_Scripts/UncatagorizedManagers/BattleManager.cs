using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    private GameObject[] _phases; // Objects for each phase in the entire battle
    //[SerializeField] private GameObject[] _dialogueObjects; // Objects containing the dialogue for each section

   // public UnityEvent PhaseFinished;
    public UnityEvent OnBeginScene;

    [SerializeField] private GameObject _dieScreen;

    private void Start()
    {
        _dieScreen.SetActive(false);

        AudioManager.Instance.PlayAudio("BattleMusic");

        var phases = FindObjectsOfType<Phase>();
        _phases = new GameObject[phases.Length];
        for (int i = 0; i < _phases.Length; i++)
            _phases[i] = phases[i].gameObject;

        SetPhasesInactive();

        Invoke("Begin", .1f);
    }

    public void StopMusic(string musicName, float duration)
    {
        AudioManager.Instance.FadeAudio(musicName, duration, 0f);
        AudioManager.Instance.StopAudio(musicName);
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene("0_MainMenu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("2_Battle");
    }

    public void Die()
    {
        StopMusic("BattleMusic", 0.5f); // Cut music off more abruptly when dying
        _dieScreen.SetActive(true);
    }

    public void SetPhasesInactive()
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Dialogue);

        foreach (GameObject phase in _phases)
        {
            phase.SetActive(false);
        }
    }

    public void Begin()
    {
        OnBeginScene?.Invoke();
    }

    public void StartNextPhase()
    {

    }
    public void StartSpecificPhase(Phase newPhase)
    {
        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Battle);
        newPhase.gameObject.SetActive(true);
        newPhase.BeginPhase(0);
    }

    public void BattleEnd()
    {
        StopMusic("BattleMusic", 3f);
    }
}
