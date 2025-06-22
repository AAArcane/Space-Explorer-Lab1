using UnityEngine;

public static class HighScoreManager
{
    private const string LEVEL_PREFIX = "LevelHighScore_";

    public static void SaveLevelScore(string levelName, int score)
    {
        int currentHigh = GetLevelHighScore(levelName);
        if (score > currentHigh)
        {
            PlayerPrefs.SetInt(LEVEL_PREFIX + levelName, score);
            PlayerPrefs.Save();
        }
    }

    public static int GetLevelHighScore(string levelName)
    {
        return PlayerPrefs.GetInt(LEVEL_PREFIX + levelName, 0);
    }

    public static void ClearAllScores()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}