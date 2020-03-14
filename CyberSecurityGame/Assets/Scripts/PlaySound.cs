using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    public List<AudioClip> clips;
    public float randompitchoffset;
    public bool PlayOnTrigger;
    private AudioSource source;
    private AudioClip originalclip;
    private float originalpitch;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        originalclip = source.clip;
        originalpitch = source.pitch;
    }

    public void Play()
    {
        source.clip = originalclip;
        SetPitch();
        source.Play();
    }

    public void Play(int clip)
    {
        if (clip < clips.Count)
        {
            source.clip = clips[clip];
            SetPitch();
            source.Play();
        }
    }

    public void PlayRandom()
    {
        source.clip = clips[Random.Range(0, clips.Count - 1)];
        SetPitch();
        source.Play();
    }

    public void PlayRandom(int start)
    {
        if (start < clips.Count)
        {
            source.clip = clips[Random.Range(start, clips.Count - 1)];
            SetPitch();
            source.Play();
        }
    }

    private void SetPitch()
    {
        source.pitch = originalpitch + Random.Range(-randompitchoffset, randompitchoffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayOnTrigger)
        {
            Play();
        }
    }
}
