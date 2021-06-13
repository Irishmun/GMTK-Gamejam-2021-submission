using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] WalkSounds;
    [SerializeField] private AudioClip[] RollSounds;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playWalkSound()
    {
        if (WalkSounds.Length > 1)
        {
            int sound = Random.Range(0, WalkSounds.Length);
            Debug.Log("selected sound: " + sound);
            audioSource.clip = WalkSounds[sound];
            audioSource.Play();//(WalkSounds[sound]);
        }
        else if (WalkSounds.Length == 1)
        {
            audioSource.clip = WalkSounds[0];
            audioSource.Play();
        }
    }

    public void playRollSound()
    {
        if (RollSounds.Length > 1)
        {
            int sound = Random.Range(0, RollSounds.Length);
            Debug.Log("selected sound: " + sound);
            audioSource.clip = RollSounds[sound];
            audioSource.Play();
        }
        else if (RollSounds.Length == 1)
        {
            audioSource.clip = RollSounds[0];
            audioSource.Play();
        }
    }

    public void playWalkSoundDelayed()
    {
        if (WalkSounds.Length > 1)
        {
            if (!audioSource.isPlaying)
            {
                int sound = Random.Range(0, WalkSounds.Length);
                Debug.Log("selected sound: " + sound);
                audioSource.clip = WalkSounds[sound];
                audioSource.Play();
            }
        }
        else if (WalkSounds.Length == 1)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = WalkSounds[0];
                audioSource.Play();
            }
        }
    }
}
