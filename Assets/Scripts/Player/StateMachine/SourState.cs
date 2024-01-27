using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SourState : IPlayerState
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

    /// <summary>
    /// Called when the player enters in this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerInput = _playerStateMachine.PlayerInput;
        _playerMovements = _playerStateMachine.PlayerMovements;

        _playerInput.onActionTriggered += OnAction;
    }

    /// <summary>
    /// Called constantly as long as the player is in this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
    public void UpdateState(PlayerStateMachine playerStateMachine)
    {

    }

    /// <summary>
    /// Called when the player exits this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
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
            // In sour state, player can move in every directions
            switch (context.action.name)
            {
                case "Movements":
                    _playerMovements.Move(context.action.ReadValue<Vector2>());
                    break;
            }
        }
    }
}
