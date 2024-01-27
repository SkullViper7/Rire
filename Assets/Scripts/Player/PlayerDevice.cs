using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDevice : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    private PlayerStateMachine _playerStateMachine;

    private void Start()
    {
        //Assign device
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        LinkPlayerToDevice();
    }

    private void LinkPlayerToDevice()
    {
        //Determine which PlayerInputController to find depending of the name of the player
        switch (gameObject.name)
        {
            case "Player1":
                TryToFindController("PlayerInputController1");
                break;
            case "Player2":
                TryToFindController("PlayerInputController2");
                break;
            case "Player3":
                TryToFindController("PlayerInputController3");
                break;
            case "Player4":
                TryToFindController("PlayerInputController4");
                break;
        }
    }

    private void TryToFindController(string name)
    {
        //Try to find the PlayerInputController for the player given, if there is no PlayerInputController for him, desactive him
        if (GameObject.Find(name) != null)
        {
            playerInput = GameObject.Find(name).GetComponent<PlayerInput>();
            _playerStateMachine.PlayerInput = playerInput;
            _playerStateMachine.ChangeState(_playerStateMachine.SourState);
            GameManager.Instance.Players.Add(this.gameObject);
            GameManager.Instance.Gamepads.Add((Gamepad)playerInput.user.pairedDevices[0]);
            //GetComponent<PlayerPause>().InitializePause();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}