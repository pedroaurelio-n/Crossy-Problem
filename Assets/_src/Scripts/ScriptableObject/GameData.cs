using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "High Score Data")]
public class GameData : ScriptableObject
{
    public bool StartGame;
    public int HighScore;
    public int CurrentScore;

    public void UpdateScoreValues(int score)
    {
        CurrentScore = score;

        if (CurrentScore > HighScore)
            HighScore = score;
    }
}
