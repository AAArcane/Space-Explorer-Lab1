using UnityEngine;
using UnityEngine.SceneManagement;

public class LostWhale : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            int currentScore = UIController.Instance.GetCurrentScore();

            // Save high score if current score is higher
            string prefsKey = currentLevel + "HighScore";
            int currentHigh = PlayerPrefs.GetInt(prefsKey, 0);
            if (currentScore > currentHigh)
            {
                PlayerPrefs.SetInt(prefsKey, currentScore);
            }

            // Existing level tracking
            if (currentLevel == "Level1") MenuManager.lastCompletedLevel = 0;
            else if (currentLevel == "Level2") MenuManager.lastCompletedLevel = 1;
            else if (currentLevel == "Level3") MenuManager.lastCompletedLevel = 2;

            SceneManager.LoadScene("Level Complete");
        }
    }
}