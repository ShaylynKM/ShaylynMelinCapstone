using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public UnityEvent OnDialogueEnded; // Any logic for when the dialogue ends, such as changing scenes.

     private GameObject _currentDialogueBox; // Dialogue box that pops up when this script is triggered
    [SerializeField] private GameObject _dialogueBoxNoTail;
    [SerializeField] private GameObject _dialogueBoxLeftTail;
    [SerializeField] private GameObject _dialogueBoxRightTailMom;
    [SerializeField] private GameObject _dialogueBoxRightTailDad;

    [SerializeField] private GameObject _leftPortraitObject;
    [SerializeField] private GameObject _rightPortraitObject;

    //[SerializeField]
    //private GameObject[] _characterPortraits; // The objects containing the character portrait(s) for each line

    //[SerializeField]
    //private TextMeshProUGUI _speakerNameText; // Name of the speaking character

    [SerializeField]
    private TextMeshProUGUI _dialogueText; // What the character is saying; should pop up inside the dialogue box

    private bool _completeCurrentSentence = false; // If we show the full sentence at once

    private Queue<DialogueLine> _lines = new Queue<DialogueLine>(); // Queue that holds all the dialogue lines for the current SO

    private bool _isTyping = false; // If text is currently being typed

    private float _loadSpeed = 0.03f; // Delay to load in the text (so the first character isn't typed instantly)

    private bool _insideFormatTag = false; // For making sure the text sounds don't play for format tags
    private DialogueTrigger _currentTrigger;
    private void Awake()
    {
        _dialogueBoxNoTail.SetActive(false);
        DialogueTrigger[] dialogueTriggers = FindObjectsOfType<DialogueTrigger>();
        foreach(DialogueTrigger dialogueTrigger in dialogueTriggers)
        {
            dialogueTrigger.DialogueTriggered.AddListener(DMTriggerDialogue);
        }
    }
    private void Start()
    {
        PlayerInputManager.Instance.NextDialogue += DisplayNextDialogueLine;
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        PlayerInputManager.Instance.NextDialogue -= DisplayNextDialogueLine;
    }

    public void DMTriggerDialogue(DialogueTrigger dialogueTrigger)
    {
        _currentTrigger = dialogueTrigger;

        ////turn off dialogue box if its already set. This can be improved later
        //if (_currentDialogueBox != null) _currentDialogueBox.SetActive(false);

        ////set new dialogue box type to current dialoge box
        //switch(_lines.Peek().DialogueBoxTailEnum)
        //{
        //    case DialogueLine.DialogueBoxTail.None:
        //        _currentDialogueBox = _dialogueBoxNoTail;
        //        break;
        //    case DialogueLine.DialogueBoxTail.Left:
        //        _currentDialogueBox = _dialogueBoxLeftTail;
        //        break;
        //    case DialogueLine.DialogueBoxTail.Right:
        //        _currentDialogueBox = _dialogueBoxRightTailMom;
        //        break;
        //}

        //_dialogueText = _currentDialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        ////reactivate dialogue box
        //_currentDialogueBox.SetActive(true);

        PlayerInputManager.Instance.ChangePlayerInputState(PlayerInputState.Dialogue); // Change the input state to the dialogue state
        StartCoroutine(WaitForDialogueLoad());
        
    }

    private IEnumerator WaitForDialogueLoad()
    {
        _dialogueText.text = ""; // Clears text
        //_speakerNameText.text = ""; // Clears speaker name
        
        yield return new WaitForSeconds(_loadSpeed); // Waits for the duration of the load speed to load in the text

        _lines.Clear(); // Empties the queue

        foreach (DialogueLine dialogueLine in _currentTrigger.Dialogue.DialogueLines)
        {
            _lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogueLine();
    }


    public void DisplayNextDialogueLine()
    {
        // Trying to figure out how to display the correct sprites for the dialogue box + however many character portraits are present, based on the line in the scriptable object
        //SpriteRenderer dialogueSprite = _dialogueBox.GetComponent<SpriteRenderer>();
        //dialogueSprite = ????


        // If we are currently typing, complete the current sentence immediately
        if (_isTyping == true)
        {
            _completeCurrentSentence = true; // Ensures the typing coroutine is stopped in TypeSentence
            _isTyping = false;

            return;
        }
        // If there are no more lines to display, end the dialogue.
        if (_lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        //turn off dialogue box if its already set. This can be improved later
        if (_currentDialogueBox != null) _currentDialogueBox.SetActive(false);

        //set new dialogue box type to current dialogue box
        switch (_lines.Peek().DialogueBoxTailEnum)
        {
            case DialogueLine.DialogueBoxTail.None:
                _currentDialogueBox = _dialogueBoxNoTail;
                break;
            case DialogueLine.DialogueBoxTail.Left:
                _currentDialogueBox = _dialogueBoxLeftTail;
                break;
            case DialogueLine.DialogueBoxTail.RightMom:
                _currentDialogueBox = _dialogueBoxRightTailMom;
                break;
            case DialogueLine.DialogueBoxTail.RightDad:
                _currentDialogueBox = _dialogueBoxRightTailDad;
                break;
        }

        Sprite leftPortrait = _lines.Peek().LeftPortraitSprite;
        if(_leftPortraitObject != null)
        {
            _leftPortraitObject.GetComponent<SpriteRenderer>().sprite = leftPortrait;
        }

        Sprite rightPortrait = _lines.Peek().RightPortraitSprite;
        if(_rightPortraitObject != null)
        {
            _rightPortraitObject.GetComponent<SpriteRenderer>().sprite = rightPortrait;

        }

        _dialogueText = _currentDialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        //reactivate dialogue box
        _currentDialogueBox.SetActive(true);

        // Dequeues the next line and updates UI elements accordingly
        DialogueLine currentLine = _lines.Dequeue();

        

        // Starts typing the next dialogue line
        StartCoroutine(TypeSentence(currentLine));
        
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        //_speakerNameText.text = dialogueLine.SpeakerName;
        _dialogueText.text = dialogueLine.Line;

        string fullText = _dialogueText.text;

        _dialogueText.maxVisibleCharacters = 0;

        _dialogueText.text = fullText;

        int dialogueLineCharLength = fullText.Length;

        _isTyping = true;

        _completeCurrentSentence = false;

        yield return new WaitForSeconds(_loadSpeed);

        int currentIndex = 0; // The index of the character we are currently typing


        // Displays each character in the dialogue line at the specified typing speed
        while (_dialogueText.maxVisibleCharacters < dialogueLineCharLength)
        {
            //if (PauseMenuManager.Instance.IsPaused)
            //{
            //    yield return new WaitUntil(() => !PauseMenuManager.Instance.IsPaused); // Avoid typing while the game is paused
            //}

            if (_completeCurrentSentence)
            {
                _dialogueText.maxVisibleCharacters = dialogueLineCharLength;

                break;
            }

            char currentTypedCharacter = fullText[currentIndex]; // Which character is about to be revealed

            // Checks to see if we are currently inside a format tag (used to keep the text sound from playing for characters that are not visually revealed
            if (currentTypedCharacter == '<')
            {
                _insideFormatTag = true;
            }
            else if (currentTypedCharacter == '>')
            {
                _insideFormatTag = false;
            }

            if (_insideFormatTag == false)
            {
                _dialogueText.maxVisibleCharacters++; // Increase the amount of visible characters one by one (only if they are not part of a format tag)

                //if (_audioSource.isPlaying == false && dialogueLine.SpeakerVoice != null)
                //{
                //    _audioSource.clip = dialogueLine.SpeakerVoice; // Use the audio from the scriptable object
                //    _audioSource.Play();
                //}
                //yield return new WaitForSecondsRealtime(_typingSpeed);
            }
            else
            {
                yield return null;
            }

            currentIndex++; // Continues to increment characters, even if they aren't being revealed (in the case of a format tag)

            yield return new WaitForSeconds(dialogueLine.TypingSpeed);

            if (currentIndex >= dialogueLineCharLength)
            {
                break; // Prevents index going out of bounds
            }
        }

        _isTyping = false;// Indicates that typing is complete.
        _completeCurrentSentence = false;
    }

    public void EndDialogue()
    {
        _currentDialogueBox.SetActive(false);

        _currentTrigger.DialogueEnds?.Invoke();
        OnDialogueEnded?.Invoke();

 
    }
}

[System.Serializable]
public class DialogueLine
{
    // If there are two portraits, use both. If there's only one, it's fine to leave one empty
    public Sprite LeftPortraitSprite;
    public Sprite RightPortraitSprite;

    [TextArea]
    public string Line; // One line of dialogue

    public float TypingSpeed = 0.05f; // How fast the text characters are being revealed in the dialogue box

    public DialogueBoxTail DialogueBoxTailEnum;

    public enum DialogueBoxTail
    {
        Left,
        RightMom,
        RightDad,
        None
    }
}
