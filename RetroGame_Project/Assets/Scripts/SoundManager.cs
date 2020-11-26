using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;

    public void PlaySound(string songName)
    {
        foreach (AudioClip c in clips)
        {
            if (c.name.StartsWith(songName))
            {
                source.clip = c;
                source.Play();
            }
        }
    }
    
}
