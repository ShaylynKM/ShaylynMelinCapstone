using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this script handles health and damage
//when the health drops below zero, broadcast an event
public class HealthComponent : MonoBehaviour, IDamageable
{
    // Check if an object is damagable before applying damage (in that object's script) (delete this later)

    [SerializeField]
    private HealthSO _healthSO;

    public UnityEvent OnDeath;
    public UnityEvent<int> OnDamage;

    private int _health;

    public void ApplyDamage(int DamageAmount)
    {
        _health -= DamageAmount;

        if (_health <= 0)
        {
            OnDeath?.Invoke();

            return;
        }
        OnDamage?.Invoke(DamageAmount);  
    }

    // Start is called before the first frame update
    void Start()
    {
        //assign private health variable to health from scriptable object
        _health = _healthSO.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}