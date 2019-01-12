using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekingPlayerVision : MonoBehaviour {

    public Transform Target;
    public float RotationSpeed;

    /// <summary>
    /// How close the cat has to be to the player to attack
    /// </summary>
    public float proximityDistanceForAttack;

    // References
    private NavMeshAgent agent;
    private AICharacterBehavior AIBehavior;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private Vector3 _directionNormalized;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AIBehavior = GetComponent<AICharacterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        _directionNormalized = (Target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_directionNormalized);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

        // move towards player as well
        agent.destination = Target.position;

        if (_direction.magnitude <= proximityDistanceForAttack)
        {
            AIBehavior.Attack();
        }
    }
}
