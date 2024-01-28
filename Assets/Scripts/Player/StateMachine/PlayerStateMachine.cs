using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// Gets or sets current state of the player.
    /// </summary>
    public IPlayerState CurrentState { get; set; }

    /// <summary>
    /// Gets default state of the player.
    /// </summary>
    public DefaultState DefaultState { get; private set; } = new();

    /// <summary>
    /// Gets dashing state of the player.
    /// </summary>
    public DashingState DashingState { get; private set; } = new();

    /// <summary>
    /// Gets falling state of the player.
    /// </summary>
    public FallingState FallingState { get; private set; } = new();

    /// <summary>
    /// Gets no move state of the player.
    /// </summary>
    public NoMoveState NoMoveState { get; private set; } = new();

    /// <summary>
    /// Gets or sets player input component of the player.
    /// </summary>
    public PlayerInput PlayerInput { get; set; }

    /// <summary>
    /// Gets player movements component of the player.
    /// </summary>
    public PlayerMovements PlayerMovements { get; private set; }

    /// <summary>
    /// Gets player dash component of the player.
    /// </summary>
    public PlayerDash PlayerDash { get; private set; }

    /// <summary>
    /// Collision that detectes collisions during dash.
    /// </summary>
    public DashCollision DashCollision { get; private set; }

    /// <summary>
    /// Gets player fall component of the player.
    /// </summary>
    public PlayerFall PlayerFall { get; private set; }

    /// <summary>
    /// Gets or sets rigidbody of the player.
    /// </summary>
    public Rigidbody Rb { get; set; }

    /// <summary>
    /// Gets or sets a value indicating that the player is laughing.
    /// </summary>
    public bool IsLaughing { get; private set; } = false;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovements = GetComponent<PlayerMovements>();
        PlayerDash = GetComponent<PlayerDash>();
        DashCollision = GetComponentInChildren<DashCollision>();
        DashCollision.gameObject.SetActive(false);
        PlayerFall = GetComponent<PlayerFall>();
        Rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Changes the actual state of the player by an other state.
    /// </summary>
    /// <param name="newState"> New state of the player. </param>
    public void ChangeState(IPlayerState newState)
    {
        // Switches to a new state
        CurrentState?.OnExit(this);

        CurrentState = newState;
        CurrentState.OnEnter(this);
    }

    /// <summary>
    /// Set this player as a player who's not laughing.
    /// </summary>
    public void IsNotAnymoreLaughing()
    {
        IsLaughing = false;
    }

    /// <summary>
    /// Set this player as the player who's laughing.
    /// </summary>
    public void MakePlayerLaughing()
    {
        IsLaughing = true;
        GameManager.Instance.LaughingPlayer = this.gameObject;
    }

    /// <summary>
    /// Stops the player never mind the actual state.
    /// </summary>
    public void StopPlayer()
    {
        switch (CurrentState) 
        {
            case DefaultState defaultState when defaultState != null:
                {
                    PlayerMovements.StopAllMovements();
                    ChangeState(NoMoveState);
                    break;
                }
            case DashingState dashingState when dashingState != null:
                {
                    PlayerDash.Raise();
                    ChangeState(NoMoveState);
                    break;
                }
            case FallingState fallingState when fallingState != null:
                {
                    PlayerFall.Raise();
                    ChangeState(NoMoveState);
                    break;
                }
            case NoMoveState noMoveState when noMoveState != null:
                {
                    break;
                }
        }
    }
}