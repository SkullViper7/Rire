using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    /// <summary>
    /// Actual screen to hide.
    /// </summary>
    [SerializeField]
    private GameObject _screenToHide;

    /// <summary>
    /// Screen to show.
    /// </summary>
    [SerializeField]
    private GameObject _screenToShow;

    /// <summary>
    /// List of the player indicators
    /// </summary>
    [SerializeField]
    private List<GameObject> _playerIndicators = new();

    [SerializeField]
    Animator _camAnim;
    [SerializeField]
    string _animation;

    /// <summary>
    /// Called when players want to go back to the previous screen.
    /// </summary>
    public void Back()
    {
        GameManager.Instance.ClearManager();
        for (int i = 0; i < _playerIndicators.Count; i++)
        {
            _playerIndicators[i].SetActive(false);
        }

        _screenToShow.SetActive(true);
        _screenToHide.SetActive(false);

        _camAnim.Play(_animation);
    }
}