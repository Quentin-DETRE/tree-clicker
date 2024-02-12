using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsManager : BaseManager
{

    public static OptionsManager Instance;


    public AudioMixer audioMixer;

    public float masterSliderValue { get; private set;} = 0.5f;
    public float musiqueSliderValue { get; private set;} = 0.6f;
    public float SFXSliderValue { get; private set;} = 0.4f;

    public ColorBlindMode mode = ColorBlindMode.Normal;
    public enum ColorBlindMode
    {
        Normal = 0,
        Protanopia = 1,
        Protanomaly = 2,
        Deuteranopia = 3,
        Deuteranomaly = 4,
        Tritanopia = 5,
        Tritanomaly = 6,
        Achromatopsia = 7,
        Achromatomaly = 8,
    }

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }

    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // // Exemple de chargement des paramètres
        // masterSliderValue = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        // musiqueSliderValue = PlayerPrefs.GetFloat("MusiqueVolume", 0.5f);
        // SFXSliderValue = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // Utilisez ces valeurs pour initialiser les sliders dans UIManager
        UIManager.Instance.SetSliderValues(masterSliderValue, musiqueSliderValue, SFXSliderValue);
        audioMixer.SetFloat("Master", Mathf.Log10(masterSliderValue) * 20);
        audioMixer.SetFloat("Musique", Mathf.Log10(musiqueSliderValue) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSliderValue) * 20);


    }

    public void SetVolume(string parameterName, float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat(parameterName, sliderValue > 0 ? volume : -80);
        
        // Sauvegarder le réglage
        PlayerPrefs.SetFloat(parameterName + "Volume", sliderValue);
        switch (parameterName)
        {
            case "Master":
                masterSliderValue = sliderValue;
                break;
            case "Musique":
                musiqueSliderValue = sliderValue;
                break;
            case "SFX":
                SFXSliderValue = sliderValue;
                break;
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeColorblindFilter(int colorblindValue)
    {

        switch(colorblindValue)
        {
            case 0:
                mode = ColorBlindMode.Normal;
                break;
            case 1:
                mode = ColorBlindMode.Protanopia;
                break;
            case 2:
                mode = ColorBlindMode.Protanomaly;
                break;
            case 3:
                mode = ColorBlindMode.Deuteranopia;
                break;
            case 4:
                mode = ColorBlindMode.Deuteranomaly;
                break;
            case 5:
                mode = ColorBlindMode.Tritanopia;
                break;
            case 6:
                mode = ColorBlindMode.Tritanomaly;
                break;
            case 7:
                mode = ColorBlindMode.Achromatopsia;
                break;
            case 8:
                mode = ColorBlindMode.Achromatomaly;
                break;
            default: 
                mode = ColorBlindMode.Normal;
                break;
        }
        
    }
}
