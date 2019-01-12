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
    public float sphereOfHearingRadius;
    /// <summary>
    /// Sensitivity of how small the player input should be in order to hear the player
    /// </summary>
    public float movementDetectionTreshold = 0.2f;

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
        // Cone of sight detection
        if (Vector3.Angle(target.position - headPosition.position, headPosition.forward) < fowAngle)
        {
            //Debug.Log("Inside cone of sight");
            RaycastHit hit;

            // Inside cone of vision of the character, but still have to check for obstacles
            Physics.Raycast(headPosition.position, target.position - headPosition.position, out hit, Mathf.Infinity, layerMask);
            if(hit.transform)
            {
                if(hit.transform.tag == "Player")
                {
                    if (!hit.transform.GetComponent<Camouflage>().GetIsCamouflaging())
                    {
                        Debug.Log("Seeing player");
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
                Debug.Log("Heard player");
            }
        }
    }
}