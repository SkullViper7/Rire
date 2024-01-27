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

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Bounds playersBounds = CalculatePlayersBounds();

        Vector3 centerPoint = playersBounds.center;

        Vector3 newPosition = centerPoint + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, _smoothTime);
    }

    Bounds CalculatePlayersBounds()
    {
        Bounds playersBounds = new Bounds(_gameManager.Players[0].transform.position, Vector3.zero);
        foreach (GameObject player in _gameManager.Players)
        {
            Renderer renderer = player.GetComponent<Renderer>();
            if (renderer != null)
            {
                playersBounds.Encapsulate(renderer.bounds);
            }
        }

        return playersBounds;
    }
}