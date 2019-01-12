using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingPlayerNoise : MonoBehaviour {

    //public Transform Target;
    public Vector3 Target;
    public GameObject Player;
    public float RotationSpeed;


    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public void Init(Vector3 SoundPosition)
    {
        Target = SoundPosition;
    }

    // Use this for initialization
    void Start () {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }


        //Target = Player.transform;
    }

    
	
	// Update is called once per frame
	void Update () {
        //find the vector pointing from our position to the target
        _direction = (Target - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    
}
