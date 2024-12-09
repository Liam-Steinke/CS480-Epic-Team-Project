using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Numerics;

public class AudioManager : MonoBehaviour
{



    public AudioMixer mixer;

    public AudioMixerGroup group;

    public Scrollbar bar, startBar;

    private float min = -80.0f;
    private float max = 0.0f;


    public void Start()
    {
        AudioSource[] all = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource a in all)
        {
            //print("audio source a = " + a.name);
            //a.outputAudioMixerGroup = mixer.outputAudioMixerGroup;
            a.outputAudioMixerGroup = group;
        }
        if (startBar != null)
        {
            updateBar(startBar);
        }
        else
        {
            updateBar(bar);
        }
    }

    public void SetVolume(Scrollbar bar)
    {
        float volume = (100 * bar.value) - 100.0f;
        //print("new volume = " + volume);
        if (mixer == null) return;
        mixer.SetFloat("masterVolume", volume);
    }

    public void updateBar(Scrollbar bar)
    {
        float current = 0.0f;
        mixer.GetFloat("masterVolume", out current);
        float newValue = Mathf.InverseLerp(min, max, current);
        //print("bar value = " + newValue);
        bar.value = newValue;
    }

    /*
    public static AudioManager singleton;

    public Scrollbar scrollbar;

    //AudioManager

    private List<AudioSource> sounds;

    private Dictionary<AudioSource, float> originalvolumes;
    private float scalar = 1.0f;


    void OnDestroy()
    {
        foreach (AudioSource a in sounds)
        {
            try
            {
                a.volume = originalvolumes[a];
            }
            catch (Exception)
            {
                continue;
            }
        }
    }

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            if (sounds == null)
            {
                sounds = new List<AudioSource>();
            }
            if (originalvolumes == null)
            {
                originalvolumes = new Dictionary<AudioSource, float>();
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
            if (!originalvolumes.ContainsKey(a))
            {
                originalvolumes.Add(a, a.volume);
            }
            a.volume = a.volume * scalar;
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
            if (originalvolumes == null)
            {
                originalvolumes = new Dictionary<AudioSource, float>();
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
            if (!originalvolumes.ContainsKey(a))
            {
                originalvolumes.Add(a, a.volume);
            }
            a.volume = a.volume + a.volume * scalar;
            sounds.Add(a);
        }
    }

    public void UpdateBar()
    {
        scrollbar.value = scalar;
    }

    public void setVolume(float newVolume)
    {
        scalar = newVolume;
        UpdateVolumes();
    }


    public void updateVolume()
    {
        scalar = scrollbar.value;
        if (scalar < .1f)
        {
            scalar = .1f;
        }
        //print("scale = " + scalar);
        UpdateVolumes();
    }

    public void updateVolume(Scrollbar s)
    {
        if (s == null)
        {
            return;
        }

        scalar = s.value;
        if (scalar < .1f)
        {
            scalar = .1f;
        }
        //print("scale = " + scalar);

        UpdateVolumes();
    }

    public float getVolume()
    {
        return scalar;
    }

    public void AddSound(AudioSource a)
    {
        if (a == null)
        {
            return;
        }
        if (!originalvolumes.ContainsKey(a))
        {
            originalvolumes.Add(a, a.volume);
        }
        a.volume = a.volume * scalar;
        if (!sounds.Contains(a))
        {
            sounds.Add(a);
        }
    }
    public void UpdateVolumes()
    {
        foreach (AudioSource a in sounds)
        {
            try
            {
                a.volume = originalvolumes[a] * scalar;
            }
            catch (Exception)
            {
                continue;
            }
        }
    }
    */
}