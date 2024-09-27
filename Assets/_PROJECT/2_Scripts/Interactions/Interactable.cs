using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject _interactBubble; // Bubble that pops up when we are in the trigger area

    public UnityEvent Interact; // Fired when we are able to interact with this object

    [SerializeField]
    private PlayerActionsSO _playerActionsSO;

    private void Awake()
    {
        if(_interactBubble != null)
            _interactBubble.SetActive(false);
    }

    private void InteractInvoker()
    {
        Interact?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _playerActionsSO.Interact.AddListener(InteractInvoker);

            if(_interactBubble != null)
                _interactBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _playerActionsSO.Interact.RemoveListener(InteractInvoker);

            if (_interactBubble != null)
                _interactBubble.SetActive(false);
        }
    }
}
