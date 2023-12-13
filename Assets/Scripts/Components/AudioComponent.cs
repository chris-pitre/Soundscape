using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private AudioSource audioSource; 
   [SerializeField] private List<AudioClip> footstepSounds;
   [SerializeField] private AudioClip keySound;
   [SerializeField] private AudioClip hurtSound;

    public void PlayFootstep(){
        AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Count)];
        audioSource.clip = clip;
        audioSource.volume = Random.Range(0.05f, 0.1f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
    public void PlayPickup(){
        audioSource.clip = keySound;
        audioSource.volume = 0.4f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void PlayHurt(){
        audioSource.clip = hurtSound;  
        audioSource.volume = 0.6f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }
}
