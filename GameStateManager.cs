using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public enum GameState
    {
        GameOn,
        Pause,
        GameOver
    }

    public GameState gameState { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }

        Instance.Pause();
    }

    public void EndGame()
    {
        if(gameState != GameState.GameOver)
        {
            //Debug.Log("Game Over");
            gameState = GameState.GameOver;
            Restart();
        }
    }

    private void Restart()
    {
        gameState = GameState.GameOn;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        gameState = GameState.GameOn;
        Time.timeScale = 1;
    }
}
