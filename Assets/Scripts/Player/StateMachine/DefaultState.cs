using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : IPlayerState
{
    /// <summary>
    /// Gets or sets state machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Gets or sets player input component of the player.
    /// </summary>
    private PlayerInput _playerInput;

    /// <summary>
    /// Gets or sets player movements component of the player.
    /// </summary>
    private PlayerMovements _playerMovements;

    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerInput = _playerStateMachine.PlayerInput;
        _playerMovements = _playerStateMachine.PlayerMovements;

        _playerInput.onActionTriggered += OnAction;
    }

    public void OnExit(PlayerStateMachine playerStateMachine)
    {
        _playerMovements.StopAllMovements();
    }

    /// <summary>
    /// Called when an input is trigger.
    /// </summary>
    /// <param name="context"> Informations about the input triggered. </param>
    private void OnAction(InputAction.CallbackContext context)
    {
        if (this == _playerStateMachine.CurrentState && !GameManager.Instance.IsGameOver && !GameManager.Instance.IsGamePaused)
        {
            // In default state, player can move in every directions and dash
            switch (context.action.name)
            {
                case "Movements":
                    _playerMovements.Move(context.action.ReadValue<Vector2>());
                    break;
                case "Dash":
                    if (context.started)
                    {
                        Debug.Log("dash");
                        _playerStateMachine.ChangeState(_playerStateMachine.DashingState);
                    }
                    break;
            }
        }
    }
}
