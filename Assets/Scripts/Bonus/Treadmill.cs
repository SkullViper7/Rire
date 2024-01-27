using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField]
    float _speed;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = new Vector3(_speed, 0, 0);
    }
}