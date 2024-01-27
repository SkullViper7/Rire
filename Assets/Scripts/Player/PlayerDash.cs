using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
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
    private float _dashForce;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();

        _playerStateMachine.DashCollision.SourHitsOtherSour += EjectOtherPlayer;
        _playerStateMachine.DashCollision.LaughingHitsSour += EjectOtherPlayer;
        _playerStateMachine.DashCollision.SourHitsLaughing += CatchLaugh;
    }

    /// <summary>
    /// Gives an impulse to the player.
    /// </summary>
    public void Dash()
    {
        _rb.velocity = Vector3.zero;
        _rb.drag = 5f;
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _rb.AddForce(transform.up * _dashForce);

        StartCoroutine(WaitUntilRaise());
    }

    /// <summary>
    /// Waits until player raises.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitUntilRaise()
    {
        // Waits during the dash
        yield return new WaitForSeconds(0.8f);

        Raise();
    }

    /// <summary>
    /// Called to stop a dash.
    /// </summary>
    public void Raise()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _rb.drag = 0f;

        // Player cans move again
        _playerStateMachine.ChangeState(_playerStateMachine.PreviousState);
    }

    /// <summary>
    /// Changes state of the other player to eject him.
    /// </summary>
    /// <param name="otherPlayerStateMachine"> Other player's state machine. </param>
    private void EjectOtherPlayer(PlayerStateMachine otherPlayerStateMachine)
    {
        otherPlayerStateMachine.ChangeState(otherPlayerStateMachine.FallingState);
    }

    /// <summary>
    /// Changes state of the other player to eject him.
    /// </summary>
    /// <param name="otherPlayerStateMachine"> Laughing player's state machine </param>
    private void CatchLaugh(PlayerStateMachine otherPlayerStateMachine)
    {
        PunchZoom.Instance.StartZoom();

        otherPlayerStateMachine.ChangeState(otherPlayerStateMachine.FallingState);
        otherPlayerStateMachine.PreviousState = otherPlayerStateMachine.SourState;
        _playerStateMachine.PreviousState = _playerStateMachine.LaughingState;
        Raise();
    }
}
