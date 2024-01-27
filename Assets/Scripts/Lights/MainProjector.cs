using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProjector : MonoBehaviour
{
    public Vector3 _offset;

    Vector3 _velocity;

    [SerializeField]
    float _smoothTime;

    [SerializeField]
    GameObject _target;

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        //Move();
        RotateToTarget(); // Add rotation to face the target
    }

    void Move()
    {
        Vector3 newPosition = _target.transform.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, _smoothTime);
    }

    void RotateToTarget()
    {
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _smoothTime);
    }
}