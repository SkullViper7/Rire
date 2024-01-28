using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    /// <summary>
    /// Gets or sets actual speed of the player.
    /// </summary>
    public float ActualSpeed { get; set; }

    /// <summary>
    /// Gets a value indicating whether player is in movement.
    /// </summary>
    public bool IsInMovement { get; private set; }

    /// <summary>
    /// Last orientation of the stick.
    /// </summary>
    private Vector3 _lastOrientation;

    /// <summary>
    /// Actual orientation of the stick.
    /// </summary>
    private Vector3 _actualOrientation;

    /// <summary>
    /// Rigidbody of the player.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Gets or sets default speed value of the player.
    /// </summary>
    [SerializeField]
    private float _defaultMoveSpeed;

    private void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _rb = _playerStateMachine.Rb;

        _lastOrientation = transform.forward;
        _actualOrientation = transform.forward;

        ActualSpeed = _defaultMoveSpeed;
    }

    /// <summary>
    /// Makes the player move.
    /// </summary>
    /// <param name="velocity"> Normalized vector of the movement. </param>
    public void Move(Vector2 velocity)
    {
        if (!GameManager.Instance.IsGameOver && !GameManager.Instance.IsGamePaused)
        {
            _lastOrientation = _actualOrientation;

            // If joystick is not in neutral position, actual orientation is the same as the joystick
            if (velocity != new Vector2(0, 0))
            {
                _actualOrientation = new Vector3(velocity.x, 0f, velocity.y);
                IsInMovement = true;
            }

            // Else keep the last orientation to don't go to the neutral position
            else
            {
                _actualOrientation = _lastOrientation;
                IsInMovement = false;
            }

            // Player orientation is the same as the stick
            transform.forward = _actualOrientation;
        }
    }

    private void FixedUpdate()
    {
        if (_playerStateMachine.CurrentState == _playerStateMachine.DefaultState && IsInMovement && 
            !GameManager.Instance.IsGameOver && !GameManager.Instance.IsGamePaused)
        {
            // Player moves
            Vector3 velocity = ActualSpeed * Time.deltaTime * _actualOrientation;
            _rb.velocity = new Vector3 (velocity.x, _rb.velocity.y, velocity.z);

            if (_playerStateMachine.IsLaughing)
            {
                _playerStateMachine.LaughingAnimator.Play("LaughingRun");
            }
            else
            {
                _playerStateMachine.AngryAnimator.Play("AngryRun");
            }
        }
        else if (_playerStateMachine.CurrentState == _playerStateMachine.DefaultState && 
                 !GameManager.Instance.IsGamePaused)
        {
            // Stops movements
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);

            if (_playerStateMachine.IsLaughing)
            {
                _playerStateMachine.LaughingAnimator.Play("LaughingIdle");
            }
            else
            {
                _playerStateMachine.AngryAnimator.Play("AngryIdle");
            }
        }
    }

    /// <summary>
    /// Stops all movements.
    /// </summary>
    public void StopAllMovements()
    {
        IsInMovement = false;

        _rb.velocity = Vector3.zero;
    }
}
