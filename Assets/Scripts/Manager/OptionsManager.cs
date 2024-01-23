using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsManager : BaseManager
{

    public static OptionsManager Instance;


    public AudioMixer audioMixer;

    private float masterSliderValue = 0.5f { get; private set;}
    private float musiqueSliderValue = 0.5f { get; private set;}
    private float SFXSliderValue = 0.5f { get; private set;}

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
        LoadSettings()
    }

    private void LoadSettings()
    {
        // // Exemple de chargement des paramètres
        // masterSliderValue = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        // musiqueSliderValue = PlayerPrefs.GetFloat("MusiqueVolume", 0.5f);
        // SFXSliderValue = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // Utilisez ces valeurs pour initialiser les sliders dans UIManager
        UIManager.Instance.SetSliderValues(masterSliderValue, musiqueSliderValue, SFXSliderValue);
    }

    public void SetVolume(string parameterName, float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat(parameterName, sliderValue > 0 ? volume : -80);
        
        // Sauvegarder le réglage
        PlayerPrefs.SetFloat(parameterName + "Volume", sliderValue);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
