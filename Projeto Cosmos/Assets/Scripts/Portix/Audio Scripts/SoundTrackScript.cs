using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] soundtracks;
    int n;

    private void Awake()
    {
        if(audioSource.clip == null)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        n = Random.Range(0, soundtracks.Length);
        audioSource.clip = soundtracks[n];
        audioSource.Play();
        StartCoroutine(ChangeSong());
    }

    IEnumerator ChangeSong()
    {
        yield return new WaitForSeconds(soundtracks[n].length);
        PlayMusic();
    }

}
