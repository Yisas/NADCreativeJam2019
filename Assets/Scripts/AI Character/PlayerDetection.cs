﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetection : MonoBehaviour
{
    public Transform target;
    /// <summary>
    /// From where to start the raycast
    /// </summary>
    public Transform headPosition;
    public float fowAngle;
    /// <summary>
    /// Sensitivity of how small the player input should be in order to hear the player
    /// </summary>
    public float movementDetectionTreshold = 0.2f;

    private AICharacterBehavior AIBehavior;

    /// <summary>
    /// Layers to ignore in the raycast, mainly the AI character itself
    /// </summary>
    private int layerMask;


    public GameObject hearingAreaVisualization;

    // Use this for initialization
    void Start()
    {
        int layerMask1 = 1 << 8;    // Cat itself
        int layerMask2 = 1 << 9;    // Room triggers
        int layerMask3 = 1 << 10;   // Floor
        layerMask = layerMask1 | layerMask2 | layerMask3;
        layerMask = ~layerMask;

        AIBehavior = GetComponent<AICharacterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cone of sight detection
        if (Vector3.Angle(target.position - headPosition.position, headPosition.forward) < fowAngle)
        {
            //Debug.Log("Inside cone of sight");
            RaycastHit hit;

            // Inside cone of vision of the character, but still have to check for obstacles
            Physics.Raycast(headPosition.position, target.position - headPosition.position, out hit, Mathf.Infinity, layerMask);
            Debug.DrawRay(headPosition.position, target.position - headPosition.position, Color.red);
            if(hit.transform)
            {
                Debug.Log("Sight raycast hit " + hit.transform.name);
                if(hit.transform.tag == "Player")
                {
                    if (!hit.transform.GetComponent<Camouflage>().GetIsCamouflaging())
                    {
                        AIBehavior.AISawPlayer();
                    }
                    
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            // Player is inside listening radius, check for movement through user input
            float h = Mathf.Abs(Input.GetAxis("Horizontal"));
            float v = Mathf.Abs(Input.GetAxis("Vertical"));
            if(h+v >= movementDetectionTreshold)
            {
                Vector3 SoundPosition = other.transform.position;
                Debug.Log("Sound position is: " + SoundPosition);
                AIBehavior.AIHeardPlayer(new Vector3(SoundPosition.x, 0.0f, SoundPosition.z));
            }
        }
    }

    public void MakeCatHearPlayer(Vector3 position)
    {
        AIBehavior.AIHeardPlayer(position);
    }

    public bool CheckIfPlayerIsInLineOfSight()
    {
        // Cone of sight detection
        if (Vector3.Angle(target.position - headPosition.position, headPosition.forward) < fowAngle)
        {
            //Debug.Log("Inside cone of sight");
            RaycastHit hit;

            // Inside cone of vision of the character, but still have to check for obstacles
            Physics.Raycast(headPosition.position, target.position - headPosition.position, out hit, Mathf.Infinity, layerMask);
            if (hit.transform)
            {
                if (hit.transform.tag == "Player")
                {
                    if (!hit.transform.GetComponent<Camouflage>().GetIsCamouflaging())
                    {
                        return true;
                    }

                }
            }
        }

        return false;
    }
}