using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider SFXSlider;
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        if (volume > 0)
        {
            audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat("Master", -80);
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (volume > 0)
        {
            audioMixer.SetFloat("Musique", Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat("Musique", -80);
        }
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        if (volume > 0)
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat("SFX", -80);
        }
    }
}
