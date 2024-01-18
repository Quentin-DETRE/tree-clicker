using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log($"Updating seeds display to {seedsAmount} \n type: {seedsAmount.GetType()} Exponent: {seedsAmount.Exponent} Coefficient: {seedsAmount.Coefficient}");
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

            }
        }
    }

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

    public void ResumeGame()
    {
        GameManager.Instance.UpdateState(GameState.Playing);
    }
    
    public void StartGame()
    {
        GameManager.Instance.UpdateState(GameState.Playing);
    }

    public void MenuGame()
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

    public void OnTreeClick()
    {
        InventoryManager.Instance.AddSeeds(EconomyManager.Instance.SeedsPerClick);
    }

    public void OnClickUpgrade(string upgradeName)
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeName);
    }
}