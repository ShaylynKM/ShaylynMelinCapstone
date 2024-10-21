using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : Singleton<InventoryManager>
{
    private Dictionary<string, GameObject> _itemDictionary = new Dictionary<string, GameObject>();

    public UnityEvent<int> OnUseItem;

    [Tooltip("Where we should be spawning inventory items. Set the layout group's container for this.")]
    [SerializeField]
    private Transform _UIPrefabLocation; 

    protected override void Awake()
    {
        _isPersistent = true;
    }

    public void AddItemToInventory(string itemName, GameObject prefab)
    {
        _itemDictionary.Add(itemName, prefab);
        Instantiate(prefab, _UIPrefabLocation); // Instantiate the prefab buttons in the UI container
        prefab.SetActive(true);
        Debug.Log("Added " + itemName + "to inventory");
    }

    public void UseItem(string itemName, GameObject prefab)
    {
        ItemSO stats = prefab.GetComponent<ItemSO>();

        if (_itemDictionary.ContainsKey(itemName))
            OnUseItem?.Invoke(stats.ItemHP); // Invoke the event, passing the amount of HP the item should heal

        prefab.SetActive(false);
    }

    private void ReplenishInventory()
    {
        foreach(var prefab in _itemDictionary.Values)
        {
            prefab.SetActive(true);
        }
    }
}
