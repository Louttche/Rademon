using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour {

    private Vector3 startPos;
    private Quaternion startRot;
    
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
	}

    //detect collision with trigger
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Death")
        {
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Animator>().Play("LOSE00", -1, 0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        }
    }
}
