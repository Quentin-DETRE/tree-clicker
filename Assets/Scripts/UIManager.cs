using UnityEngine;

public class UIManager : BaseManager
{
    public static UIManager Instance;

    private GameObject currentUI;
    private GameObject pauseUI;

    [SerializeField]
    private GameObject startUIPrefab;
    [SerializeField]
    private GameObject gameUIPrefab;
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
        HandleStateChange(GameManager.Instance.State);
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= HandleStateChange;
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

    private void LoadUI(GameObject uiPrefab)
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        if (uiPrefab != null)
        {
            currentUI = Instantiate(uiPrefab);
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
}