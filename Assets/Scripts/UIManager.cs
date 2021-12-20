using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // General info about scenes
    // 0: MainMenu
    // 1: Gameplay
    // 2: Controls
    // 3: Settings

    private Settings settings;

    private void Start()
    {
        Time.timeScale = 1; 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlaySingleGame()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        settings.SetSinglePlayer(true);
        SceneManager.LoadScene(1);
    }

    public void PlayTwoGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
