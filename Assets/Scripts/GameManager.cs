using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public static event System.Action<GameState> OnStateChanged;

    public GameState State;
    void Awake() 
    {
        Instance = this;
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
                break;
            case GameState.Playing:
                break;
            case GameState.Pause:
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    Start,
    Playing,
    Pause,
}
