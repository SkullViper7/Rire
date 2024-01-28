using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.EndButton = gameObject;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}