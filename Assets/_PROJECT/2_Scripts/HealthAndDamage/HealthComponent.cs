using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Events;

//this script handles health and damage
//when the health drops below zero, broadcast an event
public class HealthComponent : MonoBehaviour
{
    private int _currentHealth;
    private int _maxHealth = 8;

    private float _iFramesSeconds = 3;

    private bool _isInvincible = false;

    private CircleCollider2D _collider;

    public UnityEvent StartFlashingAnimation;
    public UnityEvent StopFlashingAnimation;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public UnityEvent OnDeath;
    public UnityEvent UpdateUI;

    void Start()
    {
        _currentHealth = _maxHealth;

        UpdateUIInvoker();
    }

    public void UpdateUIInvoker()
    {
        UpdateUI?.Invoke();
    }

    public void ApplyDamage(int damageAmount)
    {
        if(_isInvincible == false)
        {
            StartFlashingAnimation?.Invoke();

            _isInvincible = true; // Make the player unable to get hit again

            _currentHealth -= damageAmount; // subtract the amount of damage

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(); // If the health is zero, the object "dies" (send out event to decide what happens on death)

                return;
            }
            UpdateUIInvoker(); // Update the UI to show the new health

            Invoke("StopInvincibility", _iFramesSeconds);
        }
    }

    private void StopInvincibility()
    {
        StopFlashingAnimation?.Invoke();
        _isInvincible = false;
    }

    public void Heal(int healAmount)
    {
        if(_currentHealth < _maxHealth)
        {
            _currentHealth += healAmount; // Add the HP
        }
        UpdateUIInvoker();
    }
}