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
    /// Gets or sets last orientation of the stick.
    /// </summary>
    private Vector3 _lastOrientation;

    /// <summary>
    /// Gets or sets actual orientation of the stick.
    /// </summary>
    private Vector3 _actualOrientation;

    /// <summary>
    /// Gets or sets rigidbody of the player.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// Gets or sets default speed value of the player.
    /// </summary>
    [SerializeField]
    private float _defaultMoveSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

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
        if (IsInMovement && !GameManager.Instance.IsGameOver && !GameManager.Instance.IsGamePaused)
        {
            // Player moves
            Vector3 velocity = ActualSpeed * Time.deltaTime * _actualOrientation;
            _rb.velocity = new Vector3 (velocity.x, _rb.velocity.y, velocity.z);
        }
        else
        {
            // Stops movements
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
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
