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

    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerFall = _playerStateMachine.PlayerFall;

        _playerFall.Fall();
    }

    public void OnExit(PlayerStateMachine playerStateMachine)
    {

    }
}
