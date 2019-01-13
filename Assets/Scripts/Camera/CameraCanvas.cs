using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour {

    public Image balckoutImage;
    public float fadeSpeed;

    bool fadeOutIn = false;
    bool fadeOutInDone = false;
    bool callBack = false;

    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(fadeOutIn && fadeOutInDone)
        {
            fadeOutIn = false;
            fadeOutInDone = false;
            if (callBack)
            {
                callBack = false;
                gameController.RespawnPlayerCallback();
            }

            StartCoroutine("FadeInCoroutine");
        }
    }

    public void FadeOutIn(bool callBack = false)
    {
        fadeOutIn = true;
        StartCoroutine("FadeOutCoroutine");
        this.callBack = callBack;
    }

    public void FadeOut()
    {
        StartCoroutine("FadeOutCoroutine");
    }

    public void FadeIn()
    {
        StartCoroutine("FadeInCoroutine");
    }

    IEnumerator FadeInCoroutine()
    {
        Color c;

        for (float f = 1f; f >= 0; f -= fadeSpeed)
        {
            c = balckoutImage.color;
            c.a = f;
            balckoutImage.color = c;
            yield return null;
        }

        c = balckoutImage.color;
        c.a = 0;
        balckoutImage.color = c;
    }

    IEnumerator FadeOutCoroutine()
    {
        Color c;

        for (float f = 0f; f <= 1.0f; f += fadeSpeed)
        {
            c = balckoutImage.color;
            c.a = f;
            balckoutImage.color = c;
            yield return null;
        }

        c = balckoutImage.color;
        c.a = 1;
        balckoutImage.color = c;

        if (fadeOutIn)
        {
            fadeOutInDone = true;
        }
    }
}
