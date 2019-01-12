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
    private PlayerDetection playerDetection;
    private Vector3 lastKnownPlayerPosition;

    /// <summary>
    /// Distance to last known player position at which cat will just go back to patrol
    /// </summary>
    public float proximityToDestinationTreshold;

    public Transform Player;
    public float timer;
    private Camouflage camouflage;
    public bool GetPlayer;

    // Use this for initialization
    void Start () {

        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        AIBehavior = GetComponent<AICharacterBehavior>();
        camouflage = FindObjectOfType<Camouflage>();
        playerDetection = GetComponent<PlayerDetection>();

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

        // Set the agent to go to the last seen player position
        if (playerDetection.CheckIfPlayerIsInLineOfSight())
        {
            lastKnownPlayerPosition = Player.position;
            agent.destination = lastKnownPlayerPosition;
        }
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
            return;
        }

        if((transform.position - lastKnownPlayerPosition).magnitude <= proximityToDestinationTreshold)
        {
            AIBehavior.BackToPatroling();
        }
    }
}
