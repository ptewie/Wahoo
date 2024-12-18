using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameActionPlaySound : GameAction
{
    public AudioClip audioclip;
    public float volume;
    private AudioSource audioSource;

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        // Play the sound assocaited with this GameAction
        audioSource.PlayOneShot(audioclip, volume);        
    }
}