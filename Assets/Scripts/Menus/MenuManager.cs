using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static int lastCompletedLevel = 0;

    [Header("Level Complete References")]
    public string[] levelNames = { "Level1", "Level2", "Level3" };

    [Header("High Score UI")]
    public GameObject highScorePanel; // New panel for scores
    public TextMeshProUGUI[] levelScoreTexts; // Array for Level1,2,3 scores

    void Start()
    {
        Time.timeScale = 1f;


        // Initialize high score panel as hidden
        if (highScorePanel != null)
            highScorePanel.SetActive(false);
    }


    public void NewGame()
    {
        lastCompletedLevel = 0;
        SceneManager.LoadScene("Level1");
    }

    public void NextLevel()
    {
        lastCompletedLevel++;
        if (lastCompletedLevel < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[lastCompletedLevel]);
        }
        else
        {
            // All levels completed - return to main menu
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowHighScores()
    {
        if (highScorePanel == null) return;

        // Update text for each level
        for (int i = 0; i < levelNames.Length; i++)
        {
            int highScore = PlayerPrefs.GetInt(levelNames[i] + "HighScore", 0);
            levelScoreTexts[i].text = $"{levelNames[i]}: {highScore}";
        }

        highScorePanel.SetActive(true);
    }

    // New method: Close high score panel
    public void CloseHighScores()
    {
        if (highScorePanel != null)
            highScorePanel.SetActive(false);
    }
}