public interface IPlayerState
{
    /// <summary>
    /// Called when the player enters in this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
    public void OnEnter(PlayerStateMachine playerStateMachine);

    /// <summary>
    /// Called constantly as long as the player is in this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
    public void UpdateState(PlayerStateMachine playerStateMachine);

    /// <summary>
    /// Called when the player exits in this state.
    /// </summary>
    /// <param name="playerStateMachine"> State machine of the player. </param>
    public void OnExit(PlayerStateMachine playerStateMachine);
}