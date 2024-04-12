using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    public string previousScene;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadScene(string levelName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (levelName.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
        }
        if (levelName == "Settings")
        {
            gameManager.gameState = GameManager.GameState.Settings;
        }
        if (levelName == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        if (levelName == "GameOver")
        {
            gameManager.gameState = GameManager.GameState.GameOver;
        }
        if (levelName == "GameWin")
        {
            gameManager.gameState = GameManager.GameState.GameWin;
        }
        SceneManager.LoadScene(levelName);
    }

    public void LoadPreviousScene()
    {
        SceneManager.sceneLoaded += OnPreviousSceneLoaded;
        if (previousScene.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
            Time.timeScale = 1;
        }
        if (previousScene == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        SceneManager.LoadScene(previousScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameManager.spawnPoint = GameObject.FindWithTag("SpawnPoint");
        gameManager.MovePlayerToSpawnPoint();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnPreviousSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameManager.MovePlayerToPreviousLocation();
        SceneManager.sceneLoaded -= OnPreviousSceneLoaded;
    }
}
