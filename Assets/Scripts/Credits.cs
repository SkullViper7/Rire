using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject _mainScreen;
    [SerializeField]
    GameObject _creditsScreen;

    [SerializeField]
    Animator _camAnim;

    public void Show()
    {
        _mainScreen.SetActive(false);
        _creditsScreen.SetActive(true);

        _camAnim.Play("Credits");
    }
}