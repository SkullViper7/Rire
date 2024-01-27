using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private void Awake()
    {
        // Sets the player Input Controller prefab in don't destroy on load with a unique name
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.PlayerInputControllers.Add(gameObject);
        gameObject.name = "PlayerInputController" + GameManager.Instance.PlayerCount.ToString();
    }
}
