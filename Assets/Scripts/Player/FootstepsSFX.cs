using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;
    [SerializeField]
    List<AudioClip> _footsteps;

    Rigidbody _rb;

    bool _isMoving;
    bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Step());
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f);

        if (_rb.velocity != Vector3.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    public IEnumerator Step()
    {
        if (_isMoving && _isGrounded)
        {
            int randomClip = Random.Range(0, _footsteps.Count);

            _audioSource.PlayOneShot(_footsteps[randomClip]);
        }

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(Step());
    }
}