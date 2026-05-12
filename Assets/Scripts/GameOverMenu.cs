using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowGameOver()
    {
        Debug.Log("SHOW GAME OVER");

        gameOverPanel.SetActive(true);

        Time.timeScale = 2f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToCharacterSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UI2");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        Debug.Log("Quit Game");
    }
}