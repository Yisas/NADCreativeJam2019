using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimatorCommunicator : MonoBehaviour {

    public AICharacterBehavior aiControllerbehavior;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void KillPlayer()
    {
        gameController.KillPlayer();
    }

    public void PlayAlertSound()
    {
        aiControllerbehavior.PlayAlertSound();
    }
}
