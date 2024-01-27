using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// Gets or sets current state of the player.
    /// </summary>
    public IPlayerState CurrentState { get; set; }

    /// <summary>
    /// Gets sour state of the player.
    /// </summary>
    public SourState SourState { get; private set; } = new();

    /// <summary>
    /// Gets or sets player input component of the player.
    /// </summary>
    public PlayerInput PlayerInput { get; set; }

    /// <summary>
    /// Gets player movements component of the player.
    /// </summary>
    public PlayerMovements PlayerMovements { get; private set; }

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovements = GetComponent<PlayerMovements>();
    }

    /// <summary>
    /// Changes the actual state of the player by an other state.
    /// </summary>
    /// <param name="newState"> New state of the player. </param>
    public void ChangeState(IPlayerState newState)
    {
        // Switch to a new state
        CurrentState?.OnExit(this);

        CurrentState = newState;
        CurrentState.OnEnter(this);
    }

    private void FixedUpdate()
    {
        // Execute the actual state's comportement
        CurrentState?.UpdateState(this);
    }
}