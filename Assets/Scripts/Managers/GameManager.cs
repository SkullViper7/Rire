using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

    /// <summary>
    /// Gets or sets the maximum of players in the game.
    /// </summary>
    public int MaxPlayerCount { get; set; }

    /// <summary>
    /// Gets or sets the actual number of players in the game.
    /// </summary>
    public int PlayerCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the game is over.
    /// </summary>
    public bool IsGameOver { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating if the game is paused.
    /// </summary>
    public bool IsGamePaused { get; set; } = false;

    /// <summary>
    /// Gets or sets a list of playerInputControllers in the game.
    /// </summary>
    public List<GameObject> PlayerInputControllers { get; set; } = new();

    /// <summary>
    /// Gets or sets a list of players in the game.
    /// </summary>
    public List<GameObject> Players { get; set; } = new();

    /// <summary>
    /// Gets or sets a list of gamepads in the game.
    /// </summary>
    public List<Gamepad> Gamepads { get; set; } = new();

    /// <summary>
    /// Gets or sets the laughing player.
    /// </summary>
    public GameObject LaughingPlayer { get; set; }

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerCount = 0;
        
        // for tests
        IsGameOver = false;
    }

    /// <summary>
    /// Resets the manager, called when restart a game is chosen.
    /// </summary>
    public void ResetManager()
    {
        Players.Clear();
        Gamepads.Clear();
    }

    /// <summary>
    /// Clears the manager, called when players go back
    /// </summary>
    public void ClearManager()
    {
        // Destroys all player input controllers
        foreach (GameObject playerInputController in PlayerInputControllers)
        {
            Destroy(playerInputController);
        }
        PlayerInputControllers.Clear();

        // Resets all values
        PlayerCount = 0;
        MaxPlayerCount = 0;
        ResetManager();
    }
}
