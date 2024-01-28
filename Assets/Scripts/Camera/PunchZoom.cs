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
    Transform _targetGroup;

    [SerializeField]
    float _timeScale;

    [SerializeField]
    float _motorSpeed;

    Animator _animator;

    bool _isRumbling;

    [SerializeField]
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _clip;

    private static PunchZoom _instance = null;

    public static PunchZoom Instance => _instance;

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
        //
    }


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartZoom()
    {
        _camera.LookAt = GameManager.Instance.LaughingPlayer.transform;

        _camera.GetComponent<Animator>().SetBool("isZooming", true);
        _animator.SetBool("isZooming", true);

        _audioSource.PlayOneShot(_clip);

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

        _camera.LookAt = _targetGroup;

        SFXManager.Instance.VerifyCoroutine(SFXManager.Instance.StealApplause);
    }
}