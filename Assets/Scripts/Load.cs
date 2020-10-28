using System;
using System.Collections;
using System.Collections.Generic;
using Cursor = UnityEngine.Cursor;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class Load : MonoBehaviour
{
    /// <summary>
    /// user story 8/3 --> Loading Scene Code --> Credit: Marc-André Larouche
    /// </summary>
    
    private AsyncOperation async;
    [SerializeField] private Image filledImage;//Image for the progressbar
    [SerializeField] private Text txtPercentageText;
    [SerializeField] private bool waitForUserInput = false;// Should i wait before moving to the game?

    public void Start()
    {
        Time.timeScale = 1.0F;
        Input.ResetInputAxes();// Don't make double selections
        System.GC.Collect();// Clean everything
        async = SceneManager.LoadSceneAsync("FireFighter");// Go to scene firefighter
        async.allowSceneActivation = false;// Just go to my scene if i allow it
    }

    public void Update()
    {
        if (filledImage)
        {
            filledImage.fillAmount = async.progress + 0.1f;// Fill the right amout
        } 

        if (txtPercentageText)
        {
            txtPercentageText.text = ((async.progress + 0.1f) * 100).ToString("F2") + "%";// Now i will have 0.00%
        }
        
        if (async.progress >= 0.9f && SplashScreen.isFinished)
        {
            async.allowSceneActivation = true;
        }
    }
}
