using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("GameData Reference")]
    [SerializeField] private GameData gameData;

    [Header("UI References")]
    [SerializeField] private GameObject mainMenuVertical;
    [SerializeField] private GameObject mainMenuHorizontal;
    [SerializeField] private GameObject hudObject;
    
    [Header("Highscore References")]
    [SerializeField] private TextMeshProUGUI _highscoreVerticalTxt;
    [SerializeField] private TextMeshProUGUI _highscoreHorizontalTxt;

    private TextMeshProUGUI _highscoreTxt;

    private void Start()
    {        
        mainMenuVertical.SetActive(false);
        mainMenuHorizontal.SetActive(false);

        if (Screen.height > Screen.width)
            mainMenuVertical.SetActive(true);
        else
            mainMenuHorizontal.SetActive(true);

        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (Screen.height > Screen.width)
            _highscoreTxt = _highscoreVerticalTxt;
        else
            _highscoreTxt = _highscoreHorizontalTxt;

        _highscoreTxt.text = gameData.HighScore.ToString();
    }
}
