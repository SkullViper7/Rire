using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField]
    float _speed;

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(new Vector3(_speed, 0, 0));
    }
}