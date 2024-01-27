using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// Gets or sets current state of the player.
    /// </summary>
    public IPlayerState CurrentState { get; set; }

    /// <summary>
    /// Gets or sets previous state of the player.
    /// </summary>
    public IPlayerState PreviousState { get; set; }

    /// <summary>
    /// Gets sour state of the player.
    /// </summary>
    public SourState SourState { get; private set; } = new();

    /// <summary>
    /// Gets laughing state of the player.
    /// </summary>
    public LaughingState LaughingState { get; private set; } = new();

    /// <summary>
    /// Gets dashing state of the player.
    /// </summary>
    public DashingState DashingState { get; private set; } = new();

    /// <summary>
    /// Gets falling state of the player.
    /// </summary>
    public FallingState FallingState { get; private set; } = new();

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
    public DashCollision DashCollision;

    /// <summary>
    /// Gets player fall component of the player.
    /// </summary>
    public PlayerFall PlayerFall { get; private set; }

    /// <summary>
    /// Gets or sets rigidbody of the player.
    /// </summary>
    public Rigidbody Rb { get; set; }

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
        // Switch to a new state
        CurrentState?.OnExit(this);

        if (CurrentState == null)
        {
            PreviousState = newState;
        }
        else
        {
            PreviousState = CurrentState;
        }

        CurrentState = newState;
        CurrentState.OnEnter(this);
    }

    private void FixedUpdate()
    {
        if (gameObject.name == "Player1")
        {
            Debug.Log(CurrentState.ToString());
        }
        // Execute the actual state's comportement
        CurrentState?.UpdateState(this);
    }
}