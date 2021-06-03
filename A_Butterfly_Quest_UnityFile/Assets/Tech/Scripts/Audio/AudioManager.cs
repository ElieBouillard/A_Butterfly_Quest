using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public AudioClip[] footSteps;
    public AudioClip[] shootsSounds;
    public AudioClip[] dashsSounds;
    public AudioMixerGroup m_piste;
    public AudioSource m_audioSource;
    public AudioSource m_audioSource2;
    void Awake()
    {
        instance = this;
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = m_piste;
        }
    }
    
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Play(AudioClip item)
    {
        m_audioSource.PlayOneShot(item);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
