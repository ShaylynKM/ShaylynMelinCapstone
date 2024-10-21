using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // For UI

    private TextMeshProUGUI _text;

    [SerializeField]
    private ItemSO _stats;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _text.text = _stats.ItemName;
    }

}
