using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform player;
    private Room activeRoom;
    private PlayerDetection[] playerDetections;
    private float lastAIDetectionZoneMultiplier = 1.0f;
    private float lastAIGracePeriodMultiplier = 1.0f;

    // Use this for initialization
    void Start () {
        playerDetections = FindObjectsOfType<PlayerDetection>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        activeRoom.RespawnPlayer(player);
    }

    public void SetActiveRoom(Room room, float AIDetectionZoneMultiplier, float AIGracePeriodMultiplplier)
    {
        activeRoom = room;
        foreach (PlayerDetection playerDetection in playerDetections)
        {
            playerDetection.GetComponent<SphereCollider>().radius /= lastAIDetectionZoneMultiplier;
            playerDetection.GetComponent<SphereCollider>().radius *= AIDetectionZoneMultiplier;
            playerDetection.GetComponent<AICharacterBehavior>().gracePeriodTime /= lastAIGracePeriodMultiplier;
            playerDetection.GetComponent<AICharacterBehavior>().gracePeriodTime *= AIGracePeriodMultiplplier;
        }
        lastAIDetectionZoneMultiplier = AIDetectionZoneMultiplier;
        lastAIGracePeriodMultiplier = AIGracePeriodMultiplplier;
    }

    public void KillPlayer()
    {
        RespawnPlayer();
    }
}
