using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchZoom : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _camera;

    [SerializeField]
    float _timeScale;

    [SerializeField]
    float _motorSpeed;

    Animator _animator;

    [SerializeField]
    bool _isRumbling;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartZoom()
    {
        _camera.GetComponent<Animator>().SetBool("isZooming", true);
        _animator.SetBool("isZooming", true);

        _isRumbling = true;
    }

    private void Update()
    {
        Time.timeScale = _timeScale;

        List<Gamepad> gamepads = GameManager.Instance.Gamepads;

        if (_isRumbling)
        {
            foreach (Gamepad gamepad in gamepads)
            {
                gamepad.SetMotorSpeeds(_motorSpeed, _motorSpeed);
            }
        }
        else
        {
            foreach (Gamepad gamepad in gamepads)
            {
                gamepad.SetMotorSpeeds(0, 0);
            }
        }
    }

    public void StopRumble()
    {
        _animator.SetBool("isZooming", false);
        _camera.GetComponent<Animator>().SetBool("isZooming", false);
        _isRumbling = false;
    }
}