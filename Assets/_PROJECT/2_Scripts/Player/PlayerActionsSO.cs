using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayerActionsSO", menuName = "ScriptableObjects/PlayerActions", order = 0)]
public class PlayerActionsSO : ScriptableObject
{
    public UnityEvent Interact;
    public UnityEvent<Vector2> Movement;

    public void HandleInteract() => Interact?.Invoke();

    public void HandleMovement(Vector2 movement) => Movement?.Invoke(movement);
}
