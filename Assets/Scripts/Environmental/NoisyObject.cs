﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoisyObject : MonoBehaviour {

    private PlayerDetection[] playerDetections;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        playerDetections = FindObjectsOfType<PlayerDetection>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            foreach(PlayerDetection playerDetection in playerDetections)
                playerDetection.MakeCatHearPlayer(transform.position);

            if(!audioSource.isPlaying)
                audioSource.Play();
        }
    }
}
