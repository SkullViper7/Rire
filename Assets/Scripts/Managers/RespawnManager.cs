using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    /// <summary>
    /// List of the respawn points.
    /// </summary>
    [SerializeField]
    private List<Transform> _respawnPoints = new();

    /// <summary>
    /// List of the respawn points.
    /// </summary>
    [SerializeField]
    private float _timeBeforeRespawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Respawn(other.gameObject));
        }
    }

    /// <summary>
    /// Respawn player on a random spawn point after a delay.
    /// </summary>
    /// <param name="player"> Player to respawn. </param>
    /// <returns></returns>
    private IEnumerator Respawn(GameObject player)
    {
        PlayerStateMachine playerStateMachine = player.GetComponent<PlayerStateMachine>();

        playerStateMachine.ChangeState(playerStateMachine.NoMoveState);

        player.GetComponent<MeshRenderer>().enabled = false;

        Transform randomSpawn = _respawnPoints[Random.Range(0, _respawnPoints.Count)];

        player.transform.position = randomSpawn.position + new Vector3 (0f, 1.5f, 0f);

        player.SetActive(false);

        yield return new WaitForSeconds(_timeBeforeRespawn);

        player.SetActive(true);

        player.GetComponent<MeshRenderer>().enabled = true;

        playerStateMachine.ChangeState(playerStateMachine.DefaultState);
    }
}