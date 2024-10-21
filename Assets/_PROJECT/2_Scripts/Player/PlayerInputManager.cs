
using System;
using UnityEngine;

public class PlayerInputManager : Singleton<PlayerInputManager>
{
    private MyInputActions _inputActions;
    private PlayerController _playerController;
    public event Action<Vector2> PlayerMove;
    public event Action PlayerInteract;

    public event Action NextDialogue;
    private PlayerInputState _playerInputState;

    public void ChangePlayerInputState(PlayerInputState newState)
    {
        _playerInputState = newState;
        switch (_playerInputState)
        {
            case PlayerInputState.Dialogue:
                _inputActions.Player.Disable();
                _inputActions.PlayerBattle.Disable();
                _inputActions.PlayerDialogue.Enable();
                Debug.Log("Input state: Dialogue");
                break;
            case PlayerInputState.Battle:
                _inputActions.Player.Disable();
                _inputActions.PlayerBattle.Enable();
                _inputActions.PlayerDialogue.Disable();
                Debug.Log("Input state: Battle");
                break;
            case PlayerInputState.PlayerMove:
                _inputActions.Player.Enable();
                _inputActions.PlayerDialogue.Disable();
                _inputActions.PlayerBattle.Disable();
                Debug.Log("Input state: Move");
                break;
            
        }
    }
    protected override void Awake()
    {
        base.Awake();

        // Regular actions
        _inputActions = new MyInputActions();
        _inputActions.Player.Move.performed += (val) => PlayerMove?.Invoke(val.ReadValue<Vector2>());
        _inputActions.Player.Interact.performed += (val) => PlayerInteract?.Invoke();

        // Battle actions
        _inputActions.PlayerBattle.Move.performed += (val) => PlayerMove?.Invoke(val.ReadValue<Vector2>());

        // Dialogue actions
        _inputActions.PlayerDialogue.NextDialogue.performed += (val) => NextDialogue?.Invoke();
    }
    private void OnEnable()
    {

        ChangePlayerInputState(PlayerInputState.PlayerMove);

    }
}

public enum PlayerInputState
{
    Dialogue,
    PlayerMove,
    Battle
}
