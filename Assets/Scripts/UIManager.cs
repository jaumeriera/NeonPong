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

    // To active and deactivate menu
    private GameObject pauseMenu;

    private Settings settings;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if (pauseMenu){
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1; 
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
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
