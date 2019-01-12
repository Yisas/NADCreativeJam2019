using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoisyObject : MonoBehaviour {

    private PlayerDetection playerDetection;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        playerDetection = FindObjectOfType<PlayerDetection>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerDetection.MakeCatHearPlayer();
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
    }
}
