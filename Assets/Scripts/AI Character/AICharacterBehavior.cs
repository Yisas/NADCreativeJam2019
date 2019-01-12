using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SeekingPlayerNoise))]
[RequireComponent(typeof(SeekingPlayerVision))]
[RequireComponent(typeof(Patrol))]
public class AICharacterBehavior : MonoBehaviour {

    private SeekingPlayerNoise seekingPlayerNoiseBehavior;
    private SeekingPlayerVision seekingPlayerVisionBehavior;
    private Patrol patrolBehavior;

    private bool seeingPlayer = false;
    private bool hearingPlayer = false;
    private bool patroling = true;

	// Use this for initialization
	void Start () {
        seekingPlayerNoiseBehavior = GetComponent<SeekingPlayerNoise>();
        seekingPlayerVisionBehavior = GetComponent<SeekingPlayerVision>();
        patrolBehavior = GetComponent<Patrol>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AIHeardPlayer()
    {
        Debug.Log("AI " + transform.name + " Heard player");

        // Seeing player supercedes hearing
        if (seeingPlayer)
            return;

        hearingPlayer = true;
        patroling = false;
        seekingPlayerNoiseBehavior.enabled = true;
        patrolBehavior.enabled = false;
    }

    public void AISawPlayer()
    {
        Debug.Log("AI " + transform.name + " Seeing player");
        hearingPlayer = false;
        patroling = false;
        seeingPlayer = true;

        seekingPlayerNoiseBehavior.enabled = false;
        patrolBehavior.enabled = false;
        seekingPlayerVisionBehavior.enabled = true;
    }

    public void BackToPatroling()
    {
        hearingPlayer = false;
        seeingPlayer = false;
        patroling = true;

        seekingPlayerNoiseBehavior.enabled = false;
        seekingPlayerVisionBehavior.enabled = false;
        patrolBehavior.enabled = true;
    }
}
