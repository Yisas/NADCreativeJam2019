﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour, CanvasCallbackReceiver {

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CameraCanvas>().FadeOutIn(this);
    }
    public void Execute()
    {
        Destroy(FindObjectOfType<BackgroundMusic>().gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
