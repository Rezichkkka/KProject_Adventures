using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayCurrentLevel()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void OpenLevelList()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
