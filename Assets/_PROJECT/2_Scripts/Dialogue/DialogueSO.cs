using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>(); // Each scriptable object will contain a list of the items in DialogueLine. If I want more attributes, I need to add them in that class, not this one.

    public bool HasChoice; // If this ends in a choice or not

    public DialogueSO[] Choices; // SOs for potential choices the player could make
}
