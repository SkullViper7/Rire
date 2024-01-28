using UnityEngine;

public class FallingState : IPlayerState
{
    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Player fall component of the player.
    /// </summary>
    private PlayerFall _playerFall;

    /// <summary>
    /// Direction of the fall.
    /// </summary>
    public Vector3 Direction { get; set; }

    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerFall = _playerStateMachine.PlayerFall;

        _playerFall.Fall(Direction);
    }

    public void OnExit(PlayerStateMachine playerStateMachine)
    {

    }
}
