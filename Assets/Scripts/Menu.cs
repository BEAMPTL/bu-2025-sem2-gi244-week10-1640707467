using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void GoToCharacterSelect()
    {
        string playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        PlayerPrefs.SetString("PlayerName", playerName);

        SceneManager.LoadScene("UI2");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}