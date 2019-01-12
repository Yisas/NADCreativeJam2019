using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform player;
    private Room activeRoom;
    private PlayerDetection playerDetection;
    private float lastAIDetectionZoneMultiplier = 1.0f;

    // Use this for initialization
    void Start () {
        playerDetection = FindObjectOfType<PlayerDetection>();
	}
	
	// Update is called once per frame
	void Update () {
		// DELETEME
        if(Input.GetButtonDown("Test Button"))
        {
            RespawnPlayer();
        }
	}

    public void RespawnPlayer()
    {
        activeRoom.RespawnPlayer(player);
    }

    public void SetActiveRoom(Room room, float AIDetectionZoneMultiplier)
    {
        activeRoom = room;
        playerDetection.GetComponent<SphereCollider>().radius /= lastAIDetectionZoneMultiplier;
        playerDetection.GetComponent<SphereCollider>().radius *= AIDetectionZoneMultiplier;
        lastAIDetectionZoneMultiplier = AIDetectionZoneMultiplier;
    }
}
