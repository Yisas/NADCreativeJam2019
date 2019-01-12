using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Transform playerRespawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameController>().SetActiveRoom(this);
        }
    }

    public void RespawnPlayer(Transform playerTransform)
    {
        playerTransform.position = playerRespawnLocation.position;
    }
}
