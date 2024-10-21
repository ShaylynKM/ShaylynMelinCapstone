using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Items")]
public class ItemSO : ScriptableObject
{
    // This SO will contain all the information needed for an inventory item.

    public string ItemName; // What this item is called

    public Image ItemSprite; // The image to be displayed in the inventory

    public int ItemHP; // How much HP this item recovers when used

    [TextArea]
    public string ItemDescription; // Description of the item to use in the inventory
}
