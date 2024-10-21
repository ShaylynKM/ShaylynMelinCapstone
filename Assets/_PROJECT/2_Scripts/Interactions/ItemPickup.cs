using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Tooltip("Prefab for the inventory item.")]
    [SerializeField]
    private GameObject _UIPrefab;

    [SerializeField]
    private ItemSO _itemStats; // Scriptable object with this item's stats

    private string _itemName; // Taken from the scriptable object on this object

    private bool _collected = false;

    private void Start()
    {
        _itemName = _itemStats.ItemName; // Take the name of the item from the SO
    }

    public void PickUpItem()
    {
        if(_collected == false && _UIPrefab != null)
        {
            InventoryManager.Instance.AddItemToInventory(_itemName, _UIPrefab); // Pass this item to the inventory manager
            _collected = true;
        }
        else
        {
            return;
        }

        // This item does not need to be destroyed, as we're canonically just remembering each item.
    }
}
