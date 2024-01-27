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

    public void Zoom()
    {
        Time.timeScale = 0;
        _camera.m_Lens.FieldOfView = Mathf.Lerp(60, 30, 0.5f);
    }
}