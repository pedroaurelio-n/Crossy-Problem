using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject hudObject;

    private void Awake()
    {
        if (gameData.StartGame)
        {
            UpdateGameState(false);
            return;
        }
        
        MainMenu(false);
    }

    public void PlayGame(bool reloadScene)
    {
        gameData.StartGame = true;
        UpdateGameState(reloadScene);
    }

    public void MainMenu(bool reloadScene)
    {
        gameData.StartGame = false;
        UpdateGameState(reloadScene);
    }

    private void UpdateGameState(bool reloadScene)
    {
        if (reloadScene)
            SceneManager.LoadScene(0);

        playerObject.SetActive(gameData.StartGame);
        hudObject.SetActive(gameData.StartGame);
        menuObject.SetActive(!gameData.StartGame);
    }
}
