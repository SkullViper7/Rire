using System.Collections;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        StartCoroutine(WaitPlayers());
    }

    /// <summary>
    /// Waits players set up.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitPlayers()
    {
        yield return _gameManager.Players.Count == _gameManager.Players.Count;

        ChooseTheLaughingPlayer();
    }

    /// <summary>
    /// Chooses a random player which will be the laughing player and the other are sour.
    /// </summary>
    private void ChooseTheLaughingPlayer()
    {
        int randomIndex = Random.Range(0, _gameManager.Players.Count);

        for(int i = 0; i < _gameManager.Players.Count; i++)
        {
            PlayerStateMachine playerStateMachine = _gameManager.Players[i].GetComponent<PlayerStateMachine>();

            if (i == randomIndex)
            {
                playerStateMachine.MakePlayerLaughing();
                Debug.Log(_gameManager.Players[i].name + "rigole");
            }
            else
            {
                playerStateMachine.ChangeState(playerStateMachine.DefaultState);
                Debug.Log(_gameManager.Players[i].name + "ne rigole pas");
            }
        }
    }
}
