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
        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        if (uiPrefab != null)
        {
            currentUI = Instantiate(uiPrefab);
            if (uiPrefab == gameUIPrefab)
            {
                seedsText = currentUI.transform.Find("Shop/Money").GetComponent<Text>();
                UpdateSeedsDisplay(InventoryManager.Instance.Seeds);
            }
        }
    }

    private void ShowPauseMenu()
    {
        if (pauseUI == null && pauseUIPrefab != null)
        {
            pauseUI = Instantiate(pauseUIPrefab);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnTreeClick()
    {
        InventoryManager.Instance.AddSeeds(EconomyManager.Instance.CalculateSeedsPerClick());
    }

    public void OnClickUpgrade(string upgradeName)
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeName);
    }
}