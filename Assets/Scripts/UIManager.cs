using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    // General info about scenes
    // 0: MainMenu
    // 1: Gameplay
    // 2: Controls
    // 3: Settings

    // To active and deactivate menu
    private GameObject pauseMenu;

    // To display power ups
    private GameObject powerUp1;
    private GameObject powerUp2;
    [SerializeField] private TextMeshProUGUI powerUp1Text;
    [SerializeField] private TextMeshProUGUI powerUp2Text;
    private Color DEFAULTCOLOR = new Color(1, 1, 1);
    private string DEFAULTTEXT = "No power up";

    private Settings settings;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if (pauseMenu){
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1; 

        powerUp1 =  GameObject.FindGameObjectWithTag("Display1");
        powerUp2 =  GameObject.FindGameObjectWithTag("Display2");
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

    public void SetPowerUp1(GameObject pw) {
        Color color = pw.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        powerUp1.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        string text = pw.gameObject.GetComponent<PowerUp>().getName();
        powerUp1Text.text = text;
    }

    public void SetPowerUp2(GameObject pw) {
        Color color = pw.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        powerUp2.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        string text = pw.gameObject.GetComponent<PowerUp>().getName();
        powerUp2Text.text = text;
    }

    public void UnsetPowerUp1(){
        powerUp1.GetComponent<Renderer>().material.SetColor("_EmissionColor", DEFAULTCOLOR);
        powerUp1Text.text = DEFAULTTEXT;
    }

    public void UnsetPowerUp2(){
        powerUp2.GetComponent<Renderer>().material.SetColor("_EmissionColor", DEFAULTCOLOR);
        powerUp2Text.text = DEFAULTTEXT;
    }
}
