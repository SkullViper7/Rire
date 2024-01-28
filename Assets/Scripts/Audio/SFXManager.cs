using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance = null;

    public static SFXManager Instance => _instance;

    AudioSource _audioScource;

    [Space]
    public AudioClip StartApplause;
    public AudioClip EndApplause;
    public AudioClip StealApplause;
    public AudioClip RythmApplause;

    [Space]
    public List<AudioClip> laughs;

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        _audioScource = GetComponent<AudioSource>();
        PlaySFX(StartApplause);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        _audioScource.PlayOneShot(audioClip);
    }
}