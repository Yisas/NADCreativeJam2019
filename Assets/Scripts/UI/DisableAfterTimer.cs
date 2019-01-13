using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTimer : MonoBehaviour {

    public float time;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = time;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }
	}
}
