using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : BaseManager
{
    public static GameManager Instance;
    public GameState State { get; private set; }
    public static event System.Action<GameState> OnStateChanged;
    private Dictionary<Type, BaseManager> managers = new Dictionary<Type, BaseManager>();

    [SerializeField]
    private List<GameObject> managerPrefabs;

    private void Awake()
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }

        // Initialisation des autres managers
        InitializeManagers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (State == GameState.Pause)
            {
                UpdateState(GameState.Playing);
            }
            else if (State != GameState.Start)
            {
                UpdateState(GameState.Pause);
            }
        }
    }

    private void InitializeManagers()
    {
        foreach (var prefab in managerPrefabs)
        {
            var managerInstance = Instantiate(prefab);
            DontDestroyOnLoad(managerInstance);

            var managerComponent = managerInstance.GetComponent<BaseManager>();
            if (managerComponent != null)
            {
                managers[managerComponent.GetType()] = managerComponent;
            }
        }
    }

    void Start()
    {
        UpdateState(GameState.Start);
    }

    public void UpdateState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
        case GameState.Start:
            Time.timeScale = 0;
            break;
        case GameState.Playing:
            Time.timeScale = 1;
            break;
        case GameState.Pause:
            Time.timeScale = 0;
            break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnStateChanged?.Invoke(newState);
    }

    public T GetManager<T>() where T : BaseManager
    {
        if (managers.TryGetValue(typeof(T), out BaseManager manager))
        {
            return manager as T;
        }

        return null;
    }

    public void ChangeGameState(GameState newState)
    {
        UpdateState(newState);
    }    
}

public enum GameState
{
    Start,
    Playing,
    Pause,
}