public class DashingState : IPlayerState
{
    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Player dash component of the player.
    /// </summary>
    private PlayerDash _playerDash;

    public void OnEnter(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerDash= _playerStateMachine.PlayerDash;

        _playerStateMachine.DashCollision.gameObject.SetActive(true);

        _playerDash.Dash();
    }

    public void OnExit(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine.DashCollision.gameObject.SetActive(false);
    }
}
