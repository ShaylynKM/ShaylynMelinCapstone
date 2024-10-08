using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "ScriptableObjects/PlayerMovement")]
public class PlayerMovementSO : ScriptableObject
{
    public float MoveSpeed; // This speed can be different depending on if we're in a battle or not
    public bool Flip; // If we need the sprite to flip when moving left and right
    [field: SerializeField, Range(0, 1)] public float SlowDownSpeed { get; private set; } //makes sure nothing can change the SOs speed.
    [field: SerializeField, Range(0, 1)] public float AccelerationSpeed { get; private set; } //makes sure nothing can change the SOs speed.
}
