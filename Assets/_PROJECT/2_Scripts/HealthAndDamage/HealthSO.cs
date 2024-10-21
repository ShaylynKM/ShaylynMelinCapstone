using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthSO", menuName = "ScriptableObjects/Health")]
public class HealthSO : ScriptableObject
{
    public int Health;
    public int MaxHealth;
}
