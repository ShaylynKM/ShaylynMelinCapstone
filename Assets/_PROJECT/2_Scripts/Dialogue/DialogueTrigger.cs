using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [field: SerializeField] public DialogueSO Dialogue { get; private set; } 

    public UnityEvent<DialogueTrigger> DialogueTriggered;
    public UnityEvent DialogueEnds;
    public void StartDialogue()
    {
        DialogueTriggered?.Invoke(this);
    }
}
