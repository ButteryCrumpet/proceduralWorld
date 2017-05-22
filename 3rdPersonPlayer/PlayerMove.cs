using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    public GameObject mainCam;

    Rigidbody body;

    //initialisation event function
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
    //runs at set interval frame, moves 'character' -incomplete- 
	void FixedUpdate ()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        body.AddForce(mainCam.transform.forward * speed);
	}
}
