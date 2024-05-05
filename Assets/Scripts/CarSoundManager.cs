using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip running;
    public AudioClip overload;

    public void PlayRunningSound()
    {
        if (source.clip != running)
        {
            source.clip = running;
            source.Play();
        }
    }
    public void PlayOverloadSound()
    {
        source.clip = overload;
        source.Play();
    }
}
