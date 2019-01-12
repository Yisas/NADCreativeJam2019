using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform player;
    private Room activeRoom;

	// Use this for initialization
	void Start () {
		
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

    public void SetActiveRoom(Room room)
    {
        activeRoom = room;
    }
}
