using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {

    public Transform startRoad;
    public Transform endRoad;
    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody.velocity = new Vector3(-speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x < endRoad.position.x)
        {
            transform.position = startRoad.position;
        }
        rigidbody.velocity = new Vector3(-speed, 0, 0);
	}
}
