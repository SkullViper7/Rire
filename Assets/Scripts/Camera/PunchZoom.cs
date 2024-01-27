using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchZoom : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    CinemachineVirtualCamera _camera;

    [SerializeField]
    float _timeScale;

    public void StartZoom()
    {
        _camera.GetComponent<Animator>().Play("PunchZoom");
        GetComponent<Animator>().Play("TimeScaleDrop");
    }

    private void Update()
    {
        Time.timeScale = _timeScale;
    }
}