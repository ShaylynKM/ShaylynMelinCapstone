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
    /// </summary>

    // Put this script onto any object that triggers dialogue.

    public List<DialogueSO> DialogueObjects;

    private int _dialogueIndex = 0; // How many SOs deep we are

    [System.Serializable]
    public class DialogueTriggerEvent : UnityEvent<DialogueSO> 
    {
        // UnityEvent that takes in our dialogue scriptable object
    }

    public DialogueTriggerEvent DialogueTriggered;

    public void TriggerDialogue()
    {
        if(_dialogueIndex < DialogueObjects.Count)
        {
            DialogueTriggered?.Invoke(DialogueObjects[_dialogueIndex]); // Invoke the event, passing in the current scriptable object
            _dialogueIndex++; // Increment the index
        }
        if(_dialogueIndex == DialogueObjects.Count)
        {
            _dialogueIndex = DialogueObjects.Count - 1; // If we've reached the last object, repeat that object for future interactions.
        }
    }
}
