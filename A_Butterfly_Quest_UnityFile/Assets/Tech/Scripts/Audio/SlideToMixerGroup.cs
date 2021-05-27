using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SlideToMixerGroup : MonoBehaviour
{
    public AudioMixer currAudioMixer;

    public void SetMaster(float volume)
    {
        currAudioMixer.SetFloat("Master", volume); ;
    }
    public void SetMusic(float volume)
    {
        currAudioMixer.SetFloat("Music", volume); ;
    }
    public void SetSFX(float volume)
    {
        currAudioMixer.SetFloat("SFX", volume); ;
    }
}
