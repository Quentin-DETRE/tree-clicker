using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : BaseManager
{
    public static UIManager Instance;

    private GameObject currentUI;
    private GameObject pauseUI;

    [SerializeField]
    private GameObject startUIPrefab;
    [SerializeField]
    private GameObject gameUIPrefab;
    private Text seedsText;
    [SerializeField]
    private GameObject pauseUIPrefab;

    public Slider masterSlider;
    public Slider musiqueSlider;
    public Slider SFXSlider;

    public GameObject plusOne;

    private void Awake()
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance déjà existante, la nouvelle est détruite
        }
    }

    private void Start()
    {
        GameManager.OnStateChanged += HandleStateChange;
        InventoryManager.OnSeedsChanged += UpdateSeedsDisplay;
        HandleStateChange(GameManager.Instance.State);
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= HandleStateChange;
        InventoryManager.OnSeedsChanged -= UpdateSeedsDisplay;
    }

    private void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                if (pauseUI != null)
                {
                    Destroy(pauseUI);
                }
                LoadUI(startUIPrefab);
                break;
            case GameState.Playing:
                LoadUI(gameUIPrefab);
                HidePauseMenu();
                break;
            case GameState.Pause:
                ShowPauseMenu();
                break;
        }
    }

    private void UpdateSeedsDisplay(ScientificNumber seedsAmount)
    {
        if (seedsText != null)
        {
            //Debug.Log($"Updating seeds display to {seedsAmount} \n type: {seedsAmount.GetType()} Exponent: {seedsAmount.Exponent} Coefficient: {seedsAmount.Coefficient}");
            seedsText.text = seedsAmount.ToString();
        }
    }

    private void LoadUI(GameObject uiPrefab)
    {
        if (currentUI != null && GameManager.Instance.State != GameState.Pause)
        {
            Destroy(currentUI);
        }

        if (uiPrefab != null)
        {
            if (uiPrefab == startUIPrefab)
            {
                currentUI = Instantiate(uiPrefab);
                masterSlider = currentUI.transform.Find("SettingPanel/OpaqueBackground/MasterVolume").GetComponent<Slider>();
                masterSlider.value = OptionsManager.Instance.masterSliderValue;
                musiqueSlider = currentUI.transform.Find("SettingPanel/OpaqueBackground/MusicVolume").GetComponent<Slider>();
                musiqueSlider.value = OptionsManager.Instance.musiqueSliderValue;
                SFXSlider = currentUI.transform.Find("SettingPanel/OpaqueBackground/SFXVolume").GetComponent<Slider>();
                SFXSlider.value = OptionsManager.Instance.SFXSliderValue;
                masterSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
                musiqueSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
                SFXSlider.onValueChanged.AddListener(HandleSFXVolumeChanged);
            }
            else if (uiPrefab == gameUIPrefab)
            {
                currentUI = Instantiate(uiPrefab);
                seedsText = currentUI.transform.Find("Shop/Money/Counter").GetComponent<Text>();
                UpdateSeedsDisplay(InventoryManager.Instance.Seeds);
            }
            else if (uiPrefab == pauseUIPrefab)
            {
                pauseUI = Instantiate(uiPrefab);
                masterSlider = pauseUI.transform.Find("OpaqueBackground/MasterVolume").GetComponent<Slider>();
                masterSlider.value = OptionsManager.Instance.masterSliderValue;
                musiqueSlider = pauseUI.transform.Find("OpaqueBackground/MusicVolume").GetComponent<Slider>();
                musiqueSlider.value = OptionsManager.Instance.musiqueSliderValue;
                SFXSlider = pauseUI.transform.Find("OpaqueBackground/SFXVolume").GetComponent<Slider>();
                SFXSlider.value = OptionsManager.Instance.SFXSliderValue;
                masterSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
                musiqueSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
                SFXSlider.onValueChanged.AddListener(HandleSFXVolumeChanged);
            }
        }
    }

    // 👉 - UI Management

    private void ShowPauseMenu()
    {
        if (pauseUI == null && pauseUIPrefab != null)
        {
            LoadUI(pauseUIPrefab);
            DontDestroyOnLoad(pauseUI);
        }

        pauseUI.SetActive(true);
    }

    public void HidePauseMenu()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    public void PlayGame()
    {
        GameManager.Instance.UpdateState(GameState.Playing);
    }

    public void MainMenuGame()
    {
        GameManager.Instance.UpdateState(GameState.Start);
    }

    public void PauseGame()
    {
        GameManager.Instance.UpdateState(GameState.Pause);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // 👉 - Options Management
    public void SetSliderValues(float masterValue, float musicValue, float sfxValue)
    {
        if (masterSlider != null)
            masterSlider.value = masterValue;
        if (musiqueSlider != null)
            musiqueSlider.value = musicValue;
        if (SFXSlider != null)
            SFXSlider.value = sfxValue;
    }

    private void HandleMasterVolumeChanged(float value)
    {
        OptionsManager.Instance.SetVolume("Master", value);
    }

    private void HandleMusicVolumeChanged(float value)
    {
        OptionsManager.Instance.SetVolume("Musique", value);
    }

    private void HandleSFXVolumeChanged(float value)
    {
        OptionsManager.Instance.SetVolume("SFX", value);
    } 

    // 👉 - UI Interaction

    public void OnTreeClick()
    {
        InventoryManager.Instance.AddSeeds(EconomyManager.Instance.SeedsPerClick);       
        GameObject additionCopy = Instantiate(plusOne, new Vector3(Random.Range(-254,254), Random.Range(-504,387), 0), Quaternion.identity) as GameObject;
        additionCopy.transform.SetParent(GameObject.FindGameObjectWithTag("Tree").transform, false);
    }

    public void OnClickUpgrade(string upgradeName)
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeName);
    }
}