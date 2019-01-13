using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public AudioClip victorySound;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(victorySound, transform.position);
    }
}
