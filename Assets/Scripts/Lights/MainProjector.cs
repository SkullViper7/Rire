using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProjector : MonoBehaviour
{
    public Vector3 _offset;

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
        RotateToTarget(); // Add rotation to face the target
    }

    void RotateToTarget()
    {
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _smoothTime);
    }
}