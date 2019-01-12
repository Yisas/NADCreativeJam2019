using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform target;
    /// <summary>
    /// From where to start the raycast
    /// </summary>
    public Transform headPosition;
    public float fowAngle;

    /// <summary>
    /// Layers to ignore in the raycast, mainly the AI character itself
    /// </summary>
    private int layerMask;

    // Use this for initialization
    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        // Cone of vision
        if (Vector3.Angle(target.position - headPosition.position, headPosition.forward) < fowAngle)
        {
            Debug.Log("Inside cone of sight");
            RaycastHit hit;

            // Inside cone of vision of the character, but still have to check for obstacles
            Physics.Raycast(headPosition.position, target.position - headPosition.position, out hit, Mathf.Infinity, layerMask);
            if(hit.transform)
            {
                if(hit.transform.tag == "Player")
                {
                    Debug.Log("Detected player");
                }
            }
            else
            {
                Debug.DrawRay(headPosition.position, target.position - headPosition.position * 1000, Color.red);
            }

        }
    }
}