using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour
{

		public Transform startRoad;
		public Transform endRoad;
		public float speed;
		public float deAccelSpeed = 1;
		public float accelSpeed = 1;
		public bool insideBox;
		public bool insideCarField;

		// Use this for initialization
		void Start ()
		{
				rigidbody.velocity = new Vector3 (speed * transform.forward.x, 0, 0);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (transform.position.x < endRoad.position.x) {
						transform.position = startRoad.position;
				}
				if (rigidbody.velocity.magnitude < speed && !insideBox && !insideCarField) {
						accel ();
				}
				//rigidbody.velocity = new Vector3(-speed, 0, 0);
		}

		//deAccel
		public void deAccel ()
		{

				if (rigidbody.velocity.x * transform.forward.x > 0) {	
						rigidbody.velocity -= new Vector3 (deAccelSpeed * transform.forward.x, 0, 0);
				}
		if((rigidbody.velocity.x * transform.forward.x < 0))
		{
			rigidbody.velocity = Vector3.zero;
		}
	
				//}
		}

		//accel
		public void accel ()
		{
				if (rigidbody.velocity.x * transform.forward.x < speed) {
						rigidbody.velocity += new Vector3 (accelSpeed * transform.forward.x, 0, 0);
				}
		}

		//field in front of car
		public void OnTriggerStay (Collider col)
		{
				if (col.tag == "Player" || col.tag == "Car") {
						Debug.Log (rigidbody.velocity.magnitude);
						deAccel ();
				}
		}

		void OnCollisionEnter (Collision col)
		{
				if (col.collider.tag == "Player") {
						//Player dies...
				}
		}
}
