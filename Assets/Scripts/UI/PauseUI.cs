using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.instance.UnPause();
    }

    public void QuitToMenu()
    {
        GameManager.instance.UnPause();
        SceneManager.LoadScene("MainMenu");
    }
}