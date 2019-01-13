// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{
    
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public float timeUntilRestMin;
    public float timeUntilRestMax;
    public float restTime;

    private AICharacterBehavior AICharacterBehavior;
    private bool resting = false;
    private float nextRestTimer;
    private float restingTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AICharacterBehavior = GetComponent<AICharacterBehavior>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        if (resting)
        {
            restingTimer -= Time.deltaTime;
            if (restingTimer <= 0)
            {
                StopResting();
            }
        }
        else
        {
            nextRestTimer -= Time.deltaTime;
            if (nextRestTimer <= 0)
            {
                Rest();
            }
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void KeepMoving()
    {
        agent.isStopped = false;
    }

    public void Rest()
    {
        AICharacterBehavior.SetResting(true);
        StopMoving();
        resting = true;
        restingTimer = restTime;
    }

    private void StopResting()
    {
        AICharacterBehavior.SetResting(false);
        resting = false;
        nextRestTimer = Random.Range(timeUntilRestMin, timeUntilRestMax);
        KeepMoving();
    }
}