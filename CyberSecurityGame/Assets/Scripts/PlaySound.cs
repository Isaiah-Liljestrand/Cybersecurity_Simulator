using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    public List<AudioClip> clips;
    private AudioSource source;
    private AudioClip originalclip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        originalclip = source.clip;
    }

    public void Play()
    {
        source.clip = originalclip;
        source.Play();
    }

    public void Play(int clip)
    {
        if (clip < clips.Count)
        {
            source.clip = clips[clip];
            source.Play();
        }
    }

    public void PlayRandom()
    {
        source.clip = clips[Random.Range(0, clips.Count - 1)];
        source.Play();
    }

    public void PlayRandom(int start)
    {
        if (start < clips.Count)
        {
            source.clip = clips[Random.Range(start, clips.Count - 1)];
            source.Play();
        }
    }
}
