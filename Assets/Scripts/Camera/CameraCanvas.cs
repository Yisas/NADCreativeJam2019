using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour {

    public Image balckoutImage;
    public Image camouflageImage;
    public float fadeSpeed;

    bool fadeOutIn = false;
    bool fadeOutInDone = false;
    private CanvasCallbackReceiver callbackReceiver;

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
            if (callbackReceiver != null)
            {
                callbackReceiver.Execute();
                callbackReceiver = null;
            }

            StartCoroutine("FadeInCoroutine");
        }
    }

    public void FadeOutIn(CanvasCallbackReceiver callbackReceiver = null)
    {
        fadeOutIn = true;
        StartCoroutine("FadeOutCoroutine");
        this.callbackReceiver = callbackReceiver;
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

    public void UpdateCamouflageCooldown(float percentage)
    {
        Vector3 rectPosition = camouflageImage.rectTransform.localPosition;
        rectPosition.y = -50 + 50 * (percentage);
        camouflageImage.rectTransform.localPosition = rectPosition;
        camouflageImage.rectTransform.localScale = new Vector3(1, percentage, 1);
    }
}
