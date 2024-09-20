using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    // Need a way to store multiple different SOs for this scene and call on them depending on what the trigger is. Also need to be able to call different SOs if the same trigger is interacted with twice, for example.

    [SerializeField]
    private GameObject _dialogueBox; // Dialogue box that pops up when this script is triggered

    [SerializeField]
    private TextMeshProUGUI _speakerNameText; // Name of the speaking character

    [SerializeField]
    private TextMeshProUGUI _dialogueText; // What the character is saying; should pop up inside the dialogue box

    [SerializeField]
    public Action OnDialogueComplete; // Event triggered when the dialogue is complete

    private bool _completeCurrentSentence = false; // If we show the full sentence at once

    private Queue<DialogueLine> _lines = new Queue<DialogueLine>(); // Queue that holds all the dialogue lines for the current SO

    private bool _isTyping = false; // If text is currently being typed

    private float _loadSpeed = 0.03f; // Delay to load in the text (so the first character isn't typed instantly)

    private bool _insideFormatTag = false; // For making sure the text sounds don't play for format tags

    private void Awake()
    {
        _dialogueBox.SetActive(false);
    }

    public void TriggerDialogue(DialogueSO dialogueSO)
    {
        // Call this method in the event when triggering the dialogue from outside
        _dialogueBox.SetActive(true);
        StartCoroutine(WaitForDialogueLoad(dialogueSO));
    }

    private IEnumerator WaitForDialogueLoad(DialogueSO dialogueSO)
    {
        _dialogueText.text = ""; // Clears text
        _speakerNameText.text = ""; // Clears speaker name

        yield return new WaitForSeconds(_loadSpeed); // Waits for the duration of the load speed to load in the text

        StartDialogue(dialogueSO.DialogueLines);
    }

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        _lines.Clear(); // Empties the queue

        foreach (DialogueLine dialogueLine in dialogueLines)
        {
            _lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
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

        // Dequeues the next line and updates UI elements accordingly
        DialogueLine currentLine = _lines.Dequeue();
        _speakerNameText.text = currentLine.SpeakerName;

        StopAllCoroutines();

        // Starts typing the next dialogue line
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
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

    // Should be attached to the dialogue box button
    public void OnDialogueBoxClick()
    {
        DisplayNextDialogueLine();
    }

    public void EndDialogue()
    {
        _dialogueBox.SetActive(false);
    }
}

[System.Serializable]
public class DialogueLine
{
    public string SpeakerName; // Name of the character speaking

    [TextArea]
    public string Line; // One line of dialogue

    public SpriteRenderer SpeakerPortrait; // Image of the speaking character to be displayed with each line

    public float TypingSpeed = 0.05f; // How fast the text characters are being revealed in the dialogue box
}
