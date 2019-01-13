using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenu : MonoBehaviour, CanvasCallbackReceiver {
    public void Execute()
    {
        SceneManager.LoadScene(1);
    }

    public void Replay()
    {
        GetComponent<CameraCanvas>().FadeOutIn(this);
    }
}
