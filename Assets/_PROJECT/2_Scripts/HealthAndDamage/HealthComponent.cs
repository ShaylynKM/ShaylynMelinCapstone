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

    void Start()
    {
        //assign private health variable to health from scriptable object
        _health = _healthSO.Health;

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

    public void Heal(int healAmount)
    {
        if(_health < _healthSO.MaxHealth)
        {
            _health += healAmount; // Add the HP
        }
    }
}