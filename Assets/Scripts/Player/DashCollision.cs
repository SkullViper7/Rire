using UnityEngine;

public class DashCollision : MonoBehaviour
{
    /// <summary>
    /// State machine of the player.
    /// </summary>
    private PlayerStateMachine _playerStateMachine;

    // Observer
    public delegate void CollisionDelegate(PlayerStateMachine playerStateMachine);

    public event CollisionDelegate EjectOtherPlayer;
    public event CollisionDelegate CatchLaughing;

    private void Start()
    {
        _playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player hits other player, checks if it's a catch
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStateMachine otherPlayerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();

            IsItASourPlayerWhoHitsTheLoughing(!_playerStateMachine.IsLaughing && otherPlayerStateMachine, otherPlayerStateMachine);
        }
    }

    /// <summary>
    /// Called to make fall the other player or to catch his laugh.
    /// </summary>
    /// <param name="isItTheCase"> A value indicating if it's a sour layer who hits the laughing. </param>
    /// <param name="otherPlayerStateMachine"> The othe rplayer state machine. </param>
    private void IsItASourPlayerWhoHitsTheLoughing(bool isItTheCase, PlayerStateMachine otherPlayerStateMachine)
    {
        if (isItTheCase)
        {
            CatchLaughing?.Invoke(otherPlayerStateMachine);
        }
        else
        {
            EjectOtherPlayer?.Invoke(otherPlayerStateMachine);
        }
    }
}
