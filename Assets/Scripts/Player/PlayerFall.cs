using System.Collections;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    /// <summary>
    /// Rigidbody of the player.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Force applied to the player.
    /// </summary>
    [field: SerializeField]
    private float _fallForce;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    /// <summary>
    /// Gives an impulse to the player.
    /// </summary>
    public void Fall()
    {
        _rb.velocity = Vector3.zero;
        _rb.drag = 5f;
        transform.Rotate(90, 0, 0);
        _rb.AddForce(transform.up * _fallForce);

        StartCoroutine(WaitUntilRaise());
    }

    /// <summary>
    /// Waits until player raises.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitUntilRaise()
    {
        // Waits during the fall
        yield return new WaitForSeconds(0.8f);

        transform.Rotate(-90, 0, 0);
        _rb.drag = 0f;

        // Player cans move again
        _playerStateMachine.ChangeState(_playerStateMachine.PreviousState);
    }
}
