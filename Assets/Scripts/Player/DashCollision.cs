using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollision : MonoBehaviour
{
    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    // Observer
    public delegate void CollisionDelegate(PlayerStateMachine playerStateMachine);

    public event CollisionDelegate SourHitsOtherSour;
    public event CollisionDelegate SourHitsLaughing;
    public event CollisionDelegate LaughingHitsSour;

    private void Start()
    {
        _playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player hits other player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStateMachine otherPlayerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();

            if (_playerStateMachine.PreviousState == _playerStateMachine.SourState &&
                (otherPlayerStateMachine.PreviousState == otherPlayerStateMachine.SourState ||
                 otherPlayerStateMachine.CurrentState == otherPlayerStateMachine.SourState))
            {
                SourHitsOtherSour?.Invoke(otherPlayerStateMachine);
            }
            else if (_playerStateMachine.PreviousState == _playerStateMachine.SourState &&
                     (otherPlayerStateMachine.PreviousState == otherPlayerStateMachine.LaughingState ||
                      otherPlayerStateMachine.CurrentState == otherPlayerStateMachine.LaughingState))
            {
                SourHitsLaughing?.Invoke(otherPlayerStateMachine);
            }
            else if (_playerStateMachine.PreviousState == _playerStateMachine.LaughingState &&
                     (otherPlayerStateMachine.PreviousState == otherPlayerStateMachine.SourState ||
                      otherPlayerStateMachine.CurrentState == otherPlayerStateMachine.SourState))
            {
                LaughingHitsSour?.Invoke(otherPlayerStateMachine);
            }
        }
    }
}
