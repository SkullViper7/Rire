using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    private void Start()
    {
        Invoke("Menu", 1.5f);
    }

    void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}