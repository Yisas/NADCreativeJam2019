using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimatorCommunicator : MonoBehaviour {

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void KillPlayer()
    {
        gameController.KillPlayer();
    }
}
