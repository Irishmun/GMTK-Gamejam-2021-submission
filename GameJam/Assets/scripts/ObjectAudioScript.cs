using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectAudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip ActivatedSound, DeActivatedSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayActivatedSound()
    {
        playSound(ActivatedSound);
    }

    public void PlayDeactivatedSound()
    {
        playSound(DeActivatedSound);
    }

    private void playSound(AudioClip sound)
    {
        //audioSource.clip = sound;
        audioSource.PlayOneShot(sound);
        audioSource.Play();
    }
}
