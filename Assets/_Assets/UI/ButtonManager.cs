using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public GameObject settingsMenu, mainMenuScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene("02");
        Time.timeScale = 1;
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        mainMenuScreen.SetActive(false);
        Time.timeScale = 0;
    }
    public void Home()
    {
        SceneManager.LoadScene("01");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void Close()
    {
        settingsMenu.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    public void Exit()
    {
        Debug.Log("Game exited");
        Application.Quit();
    }


    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
