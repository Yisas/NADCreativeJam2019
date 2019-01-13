using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorCommunicator : MonoBehaviour {

    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonCharacter;

   public void PlayStepSound()
    {
        thirdPersonCharacter.PlayStepSound();
    }
}
