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
    /// Called when players want to go back to the previous screen.
    /// </summary>
    public void Back()
    {
        _screenToShow.SetActive(true);
        _screenToHide.SetActive(false);
    }
}
