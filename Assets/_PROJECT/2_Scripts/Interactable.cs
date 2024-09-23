using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Interact event
    /// </summary>

    private MyInputActions _input;

    private bool _hasCollided = false;

    [SerializeField]
    private bool _collisionBased; // If entering a trigger is required to interact. Set in inspector

    public UnityEvent Interact; // Fired when we are able to interact with this object

    [SerializeField]
    private GameObject _interactBubble; // Bubble that appears to show you can interact with something when you walk into the collider

    private void Awake()
    {
        _input = new MyInputActions();

        _interactBubble.SetActive(false);
    }

    private void OnEnable()
    {
        // Subscribe to our input action

        _input.Enable();

        _input.Player.Interact.performed += OnInteract;
    }
    private void OnDisable()
    {
        // Unsubscribe from our input action

        _input.Disable();

        _input.Player.Interact.performed -= OnInteract;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // If we need to collide with a trigger and we have, or if we do not need to collide with a trigger, fire the event.
        if(_collisionBased == true && _hasCollided == true || _collisionBased == false)
        {
            Interact?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_collisionBased == true)
            {
                _hasCollided = true;
                _interactBubble.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_collisionBased == true)
            {
                _hasCollided = false;
                _interactBubble.SetActive(false);
            }
        }

    }
}
