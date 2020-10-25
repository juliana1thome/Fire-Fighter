using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("Loading");//and when it go to the next scene it will call the function loadlevel and it will load the level
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
