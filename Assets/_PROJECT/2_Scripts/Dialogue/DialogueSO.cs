using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/Dialogue")]
[System.Serializable]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>(); // Each scriptable object will contain a list of the items in DialogueLine. If I want more attributes, I need to add them in that class, not this one. (Unless I only need one in this object, like a bool for the whole SO)
}
