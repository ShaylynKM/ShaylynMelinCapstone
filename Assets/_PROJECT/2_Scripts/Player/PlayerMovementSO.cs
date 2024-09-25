using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "ScriptableObjects/PlayerMovement")]
public class PlayerMovementSO : ScriptableObject
{
    public float MoveSpeed; // This speed can be different depending on if we're in a battle or not
    public bool Flip; // If we need the sprite to flip when moving left and right
}
