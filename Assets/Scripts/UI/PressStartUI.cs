using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class PressStartUI : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}