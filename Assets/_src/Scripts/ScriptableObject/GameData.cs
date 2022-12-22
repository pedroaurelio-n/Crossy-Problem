using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "High Score Data")]
public class GameData : ScriptableObject
{
    public bool StartGame;
    public int CurrentScore;

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void UpdateScoreValues(int score)
    {
        CurrentScore = score;

        if (CurrentScore > GetHighScore())
            PlayerPrefs.SetInt("HighScore", score);
    }
}
