using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSettings()
    {
        SceneManager.LoadScene("dummy");
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("movement test");
    }
}

