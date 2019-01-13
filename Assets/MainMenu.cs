using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, CanvasCallbackReceiver {
    public void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGame()
    {
        GetComponent<CameraCanvas>().FadeOutIn(this);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
