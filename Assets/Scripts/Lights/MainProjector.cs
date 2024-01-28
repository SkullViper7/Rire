using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProjector : MonoBehaviour
{
    [SerializeField]
    float _smoothTime;

    GameObject _target;

    private void FixedUpdate()
    {
        _target = GameManager.Instance.LaughingPlayer;
        RotateToTarget(); // Add rotation to face the target
        Debug.DrawLine(transform.position, transform.forward.normalized * 3, Color.green, 0.1f);
    }

    void RotateToTarget()
    {
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _smoothTime);
    }
}