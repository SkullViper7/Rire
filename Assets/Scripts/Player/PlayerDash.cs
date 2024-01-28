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

        _playerStateMachine.DashCollision.EjectOtherPlayer += EjectOtherPlayer;
        _playerStateMachine.DashCollision.CatchLaughing += StealLaughFrom;
    }

    /// <summary>
    /// Gives an impulse to the player.
    /// </summary>
    public void Dash()
    {
        _rb.velocity = Vector3.zero;
        _rb.drag = 5f;

        if (_playerStateMachine.IsLaughing)
        {
            _playerStateMachine.LaughingAnimator.Play("LaughingDash");
        }
        else
        {
            _playerStateMachine.AngryAnimator.Play("AngryDash");
        }

        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);
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
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        _rb.drag = 0f;

        // Player cans move again
        _playerStateMachine.ChangeState(_playerStateMachine.DefaultState);
    }

    /// <summary>
    /// Ejects the other player.
    /// </summary>
    /// <param name="otherPlayerStateMachine"> Other player's state machine. </param>
    private void EjectOtherPlayer(PlayerStateMachine otherPlayerStateMachine)
    {
        otherPlayerStateMachine.FallingState.Direction = transform.forward;
        otherPlayerStateMachine.ChangeState(otherPlayerStateMachine.FallingState);
    }

    /// <summary>
    /// Steals the laugh of the other player and ejects him.
    /// </summary>
    /// <param name="oldLaughingPlayerStateMachine"> The old laughing player's state machine </param>
    private void StealLaughFrom(PlayerStateMachine oldLaughingPlayerStateMachine)
    {
        PunchZoom.Instance.StartZoom();

        oldLaughingPlayerStateMachine.IsNotAnymoreLaughing();
        oldLaughingPlayerStateMachine.ChangeState(oldLaughingPlayerStateMachine.FallingState);

        _playerStateMachine.MakePlayerLaughing();
        Raise();
    }
}
