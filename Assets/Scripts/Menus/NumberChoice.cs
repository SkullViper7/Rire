using UnityEngine;
using UnityEngine.InputSystem;

public class NumberChoice : MonoBehaviour
{
    /// <summary>
    /// Screen where player can choose the number of players.
    /// </summary>
    [SerializeField]
    private GameObject _numberChoiceScreen;

    /// <summary>
    /// Screen where players can join the game.
    /// </summary>
    [SerializeField]
    private GameObject _lobbyScreen;

    /// <summary>
    /// Manager of the inputs.
    /// </summary>
    [SerializeField]
    private PlayerInputManager _playerInputManager;

    /// <summary>
    /// Called when a number of players is choosen.
    /// </summary>
    /// <param name="_numberOfPlayer"></param>
    public void Click(int numberOfPlayer)
    {
        //Sets the max number of player and shows the lobby screen
        GameManager.Instance.MaxPlayerCount = numberOfPlayer;
        _lobbyScreen.SetActive(true);
        _playerInputManager.EnableJoining();
        _numberChoiceScreen.SetActive(false);
    }
}
