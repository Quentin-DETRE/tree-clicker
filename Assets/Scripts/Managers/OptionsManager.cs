using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsManager : BaseManager
{

    public static OptionsManager Instance;


    public AudioMixer audioMixer;

    public float masterSliderValue;
    public float musiqueSliderValue ;
    public float SFXSliderValue;

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
        masterSliderValue = 0.5f;
        musiqueSliderValue = 0.5f;
        SFXSliderValue = 0.5f;
    }

    public void SetMasterVolume()
    {
        float volume = UIManager.Instance.masterSlider.value;
        masterSliderValue = volume;
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
        float volume = UIManager.Instance.musiqueSlider.value;
        musiqueSliderValue = volume;
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
        float volume = UIManager.Instance.SFXSlider.value;
        SFXSliderValue = volume;
        if (volume > 0)
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat("SFX", -80);
        }
    }

    public void SetFullscreen ()
    {
        Toggle isFullscreen = gameObject.GetComponent<Toggle>();
        Screen.fullScreen = isFullscreen.value;
    }

}
