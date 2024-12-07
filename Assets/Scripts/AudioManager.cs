using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager singleton;

    public Scrollbar scrollbar;

    //AudioManager

    private List<AudioSource> sounds;
    private float currentVolume = 1.0f;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            if (sounds == null)
            {
                sounds = new List<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        AudioSource[] all = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource a in all)
        {
            a.volume = currentVolume;
            sounds.Add(a);
        }
    }
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
            if (sounds == null)
            {
                sounds = new List<AudioSource>();
            }
        }

        AudioSource[] all = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource a in all)
        {
            a.volume = currentVolume;
            sounds.Add(a);
        }
    }

    public void UpdateBar()
    {
        scrollbar.value = currentVolume;
    }

    public void setVolume(float newVolume)
    {
        currentVolume = newVolume;
        UpdateVolumes();
    }


    public void updateVolume()
    {
        currentVolume = scrollbar.value;
        UpdateVolumes();
    }

    public void updateVolume(Scrollbar s)
    {
        if (s == null)
        {
            return;
        }
        currentVolume = s.value;
        UpdateVolumes();
    }

    public float getVolume()
    {
        return currentVolume;
    }

    public void AddSound(AudioSource a)
    {
        if (a == null)
        {
            return;
        }
        a.volume = currentVolume;
        if (!sounds.Contains(a))
        {
            sounds.Add(a);
        }
    }

    // public void updateSound(AudioSource a)
    // {
    //     a.volume = currentVolume;
    // }

    public void UpdateVolumes()
    {
        foreach (AudioSource a in sounds)
        {
            a.volume = currentVolume;
        }
    }
}