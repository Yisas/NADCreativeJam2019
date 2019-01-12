using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyObject : MonoBehaviour {

    private PlayerDetection playerDetection;

	// Use this for initialization
	void Start () {
        playerDetection = FindObjectOfType<PlayerDetection>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerDetection.MakeCatHearPlayer();
        }
    }
}
