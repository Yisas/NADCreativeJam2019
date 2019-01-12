using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekingPlayerVision : MonoBehaviour {
    /// <summary>
    /// How close the cat has to be to the player to attack
    /// </summary>
    public float proximityDistanceForAttack;

    // References
    private NavMeshAgent agent;
    private AICharacterBehavior AIBehavior;


    public GameObject Player;
    public float timer;
    private Camouflage camouflage;
    public bool GetPlayer;

    // Use this for initialization
    void Start () {

        if(Player == null)
        {
            Player = Player = GameObject.FindGameObjectWithTag("Player");
        }
        agent = GetComponent<NavMeshAgent>();
        AIBehavior = GetComponent<AICharacterBehavior>();
        camouflage = FindObjectOfType<Camouflage>();

        StartCoroutine(seekPlayer());
    }

    IEnumerator seekPlayer()
    {
        yield return new WaitForSeconds(timer);
        if (!camouflage.GetIsCamouflaging() && !AIBehavior.IsInGracePeriod())
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

        Vector3 direction = (Player.transform.position - transform.position);

        if (direction.magnitude <= proximityDistanceForAttack)
        {
            AIBehavior.Attack();
        }
    }
}
