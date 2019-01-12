using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Transform playerRespawnLocation;
    public float AIDetectionZoneMultiplier = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameController>().SetActiveRoom(this, AIDetectionZoneMultiplier);
        }
    }

    public void RespawnPlayer(Transform playerTransform)
    {
        playerTransform.position = playerRespawnLocation.position;
    }
}
