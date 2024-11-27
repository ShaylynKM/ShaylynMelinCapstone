using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    /// <summary>
    /// Events in this script: 
    /// - DialogueTriggered (fired when something triggers a specific section of dialogue. Fired again every time a new SO is used.)
    /// 
    /// TriggerDialogue() should be called upon interact with a trigger object, or in response to any condition being met that requires dialogue to trigger.
    /// 
    /// UseAlternateDialogue() should be called when a condition is meant to switch to different lines of dialogue, such as an NPC giving instructions and then responding after the player carries out those instructions.
    /// </summary>

    // Put this script onto any object that triggers dialogue.

    public bool alt; // testing

    [field: SerializeField] public DialogueSO Dialogue { get; private set; } 
    //public List<DialogueSO> DialogueObjects;

    //public List<DialogueSO> AlternateDialogue; // If there's different dialogue after a condition changes

    private int _dialogueIndex = 0; // How many SOs deep we are

    public UnityEvent<DialogueTrigger> DialogueTriggered;
    public UnityEvent DialogueEnds;
    public void StartDialogue()
    {
        DialogueTriggered?.Invoke(this);
    }

    //public void DTTriggerDialogue()
    //{
    //    if(_dialogueIndex < DialogueObjects.Count)
    //    {
    //        DialogueTriggered?.Invoke(DialogueObjects[_dialogueIndex]); // Invoke the event, passing in the current scriptable object

    //        _dialogueIndex++; // Increment the index
    //    }
    //    if (_dialogueIndex == DialogueObjects.Count)
    //    {
    //        _dialogueIndex = DialogueObjects.Count - 1; // If we've reached the last object, repeat that object for future interactions.
    //    }
    //}

    //public void UseAlternateDialogue()
    //{
    //    DialogueObjects = AlternateDialogue; // Switch which objects we are using. Call with an event based on a condition.
    //}
}
