using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    /// <summary>
    /// Launch the scene given.
    /// </summary>
    /// <param name="sceneName"> Scene to load. </param>
    public void LaunchAScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
