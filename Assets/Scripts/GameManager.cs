using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject LossWindow; 
    public GameObject WinWindow;
    public GameObject PauseMenu;    
    public static GameManager instance;

    
    private bool isPaused = false; 
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        PauseMenu.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Loss()
    {
        LossWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        WinWindow.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;          
        PauseMenu.SetActive(true);     
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;           
        PauseMenu.SetActive(false);    
        isPaused = false;
    }
}

