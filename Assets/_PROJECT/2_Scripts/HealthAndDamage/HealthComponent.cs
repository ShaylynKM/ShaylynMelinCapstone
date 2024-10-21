using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this script handles health and damage
//when the health drops below zero, broadcast an event
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private HealthSO _healthSO;

    public UnityEvent OnDeath;
    //public UnityEvent<int> OnDamage;
    public UnityEvent<int> UpdateUI;

    private int _health;
    private int _maxHealth;

    void Start()
    {
        //assign private health variable to health from scriptable object
        _health = _healthSO.Health;
        _maxHealth = _healthSO.MaxHealth;

        UpdateUI?.Invoke(_health);
    }

    public void ApplyDamage(int damageAmount)
    {
        _health -= damageAmount; // subtract the amount of damage

        if (_health <= 0)
        {
            OnDeath?.Invoke(); // If the health is zero, the object "dies" (send out event to decide what happens on death)

            return;
        }
        UpdateUI?.Invoke(_health); // Update the UI to show the new health
    }

    public void ApplyHealing(int healAmount)
    {
        if(_health + healAmount > _maxHealth)
        {
            _health = _maxHealth; // Do not surpass the maximum health amount
        }
        else
        {
            _health += healAmount; // Add the amount of HP to heal
        }

        UpdateUI?.Invoke(_health); // Update the UI to show the new health
    }
}