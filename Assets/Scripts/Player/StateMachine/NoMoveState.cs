using UnityEngine;

public class NoMoveState : IPlayerState
{
    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
    }

    public void OnExit(PlayerStateMachine playerStateMachine)
    {

    }
}
