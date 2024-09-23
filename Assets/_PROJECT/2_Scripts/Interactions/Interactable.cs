using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private bool _hasCollided = false;

    [SerializeField]
    private GameObject _interactBubble; // Bubble that pops up when we are in the trigger area

    [SerializeField]
    private bool _collisionBased; // If entering a trigger is required to interact. Set in inspector

    public UnityEvent Interact; // Fired when we are able to interact with this object

    [SerializeField]
    private PlayerActionsSO _playerActionsSO;

    private void Awake()
    {
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
            _interactBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _playerActionsSO.Interact.RemoveListener(InteractInvoker);
            _interactBubble.SetActive(false);
        }
    }
}
