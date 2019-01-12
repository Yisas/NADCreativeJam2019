using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class SeekingPlayerVision : MonoBehaviour {

    public GameObject Player;
    public float timer;
    public bool PlayerVisible;
    public bool GetPlayer;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        if(Player == null)
        {
            Player = Player = GameObject.FindGameObjectWithTag("Player");
        }
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(seekPlayer());

    }

    IEnumerator seekPlayer()
    {
        yield return new WaitForSeconds(timer);
        if (PlayerVisible)
        {
            GetPlayer = true;
        }
        //tbd
        else GetPlayer = false;

    }

    void GotoPlayer()
    {
        // Returns if no points have been set up
        if (Player == null)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = Player.transform.position;

       
    }

    // Update is called once per frame
    void Update () {
        if (GetPlayer)
        {
            GotoPlayer();
        }
	}
}
