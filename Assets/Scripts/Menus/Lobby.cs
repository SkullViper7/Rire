using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lobby : MonoBehaviour
{
    /// <summary>
    /// Manager of the inputs.
    /// </summary>
    [SerializeField]
    private PlayerInputManager _playerInputManager;

    /// <summary>
    /// Button to launch the game when all players are connected.
    /// </summary>
    [SerializeField]
    private GameObject _launchGameButton;

    /// <summary>
    /// Instructions of the lobby.
    /// </summary>
    [SerializeField]
    private GameObject _instructions;

    /// <summary>
    /// List of the player indicators
    /// </summary>
    [SerializeField]
    private List<GameObject> _playerIndicators = new();

    public void OnPlayerJoiningLobby()
    {
        // Adds a player in the count if a player joines and disables joining if the maximum of players is reached
        if (GameManager.Instance.PlayerCount + 1 >= GameManager.Instance.MaxPlayerCount)
        {
            _playerInputManager.DisableJoining();
            GameManager.Instance.PlayerCount++;
        }
        else
        {
            GameManager.Instance.PlayerCount++;
        }

        // Actives the player indicator associated to the player who has joined
        _playerIndicators[GameManager.Instance.PlayerCount - 1].SetActive(true);

        if (GameManager.Instance.PlayerCount == GameManager.Instance.MaxPlayerCount)
        {
            // Actives the button to launch the game when everyone is connected
            _instructions.SetActive(false);
            _launchGameButton.SetActive(true);
        }
    }
}
