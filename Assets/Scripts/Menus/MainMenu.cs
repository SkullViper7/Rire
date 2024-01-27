using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Screen where players can play or quit the game.
    /// </summary>
    [SerializeField]
    private GameObject _mainMenuScreen;

    /// <summary>
    /// Screen where player can choose the number of players.
    /// </summary>
    [SerializeField]
    private GameObject _numberChoiceScreen;

    /// <summary>
    /// Launches the number choice screen.
    /// </summary>
    public void Play()
    {
        _numberChoiceScreen.SetActive(true);
        _mainMenuScreen.SetActive(false);
    }
}
