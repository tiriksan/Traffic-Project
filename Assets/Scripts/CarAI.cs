﻿using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {

    public Transform startRoad;
    public Transform endRoad;
    public float speed;
	public float deAccelSpeed = 1;
	public float accelSpeed = 1;

	// Use this for initialization
	void Start () 
	{
        rigidbody.velocity = new Vector3(-speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x < endRoad.position.x)
        {
            transform.position = startRoad.position;
        }
        //rigidbody.velocity = new Vector3(-speed, 0, 0);
	}

	//deAccel
	public void deAccel()
	{
		//for (int i = 0; i > speed; i++) {
		if (rigidbody.velocity.x < 0) 
		{	
			rigidbody.velocity += new Vector3 (deAccelSpeed, 0, 0);
		}	
	
		//}
	}

	//accel
	public void accel()
	{
		if (rigidbody.velocity.x < speed * transform.forward.x)
		{
			rigidbody.velocity -= new Vector3(accelSpeed * transform.forward.x, 0,0);
		}
	}

    void OnCollisionEnter (Collision col)
    {
        if(col.collider.tag == "Player")
        {
            //Player dies...
        }
    }
}
