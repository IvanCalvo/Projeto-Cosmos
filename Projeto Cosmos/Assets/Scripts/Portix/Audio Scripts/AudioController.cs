using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] dropsAudio;
    public AudioClip[] hoverAudio;
    public AudioClip[] shootAudio;
    public AudioClip[] shootMissileAudio;
    public AudioClip[] completeMissionAudio;
    public AudioClip clickAudio;
    public AudioClip backAudio;

    public void HoverSound()
    {
        int n = Random.Range(0, hoverAudio.Length);
        audioSource.clip = hoverAudio[n];
        audioSource.Play();
    }

    public void ClickSound()
    {
        audioSource.clip = clickAudio;
        audioSource.volume = 0.0001f;
        audioSource.Play();
    }

    public void BackSound()
    {
        audioSource.clip = backAudio;
        audioSource.volume = 0.001f;
        audioSource.Play();
    }

    public void ShootSound()
    {
        int n = Random.Range(0, shootAudio.Length);
        audioSource.volume = 0.1f;
        audioSource.clip = shootAudio[n];
        audioSource.Play();
    }

    public void CompleteMissionSound()
    {
        int n = Random.Range(0, completeMissionAudio.Length);
        audioSource.volume = 0.1f;
        audioSource.clip = completeMissionAudio[n];
        audioSource.Play();
    }

    public void ShootMissileSound()
    {
        int n = Random.Range(0, shootMissileAudio.Length);
        audioSource.clip = shootMissileAudio[n];
        audioSource.Play();
    }

    public void PlayPickUpSound()
    {
        int n = Random.Range(0, dropsAudio.Length);
        audioSource.clip = dropsAudio[n];
        audioSource.Play();
    }

    public void BoostSound()
    {

    }
}
