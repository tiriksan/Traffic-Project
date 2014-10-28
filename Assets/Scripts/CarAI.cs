using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour
{

    public Transform startRoad;
    public Transform endRoad;
    public float speed;
    public float deAccelSpeed = 0.1f;
    public float accelSpeed = 1;
    public bool insideBox;
    public bool insideCarField;

    // Use this for initialization
    void Start()
    {
        rigidbody.velocity = new Vector3(speed * transform.forward.x, 0, speed * transform.forward.z);
    }

    // Update is called once per frame
    void Update()
    {
        //If the car has hit the end of it's path it will be moved to the start
        if (transform.position.x * -transform.forward.x <= endRoad.position.x * -transform.forward.x && transform.position.z * -transform.forward.z <= endRoad.position.z * -transform.forward.z)
        {
            transform.position = startRoad.position;
			transform.forward = startRoad.forward;
			rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
        }
        //If the char hasn't hit it's max speed or is not inside a deAccel field it will accel
        if (rigidbody.velocity.magnitude < speed && !insideBox && !insideCarField)
            accel();
    }

    //deAccel
    public void deAccel()
    {

        if (rigidbody.velocity.x * transform.forward.x > 0 || rigidbody.velocity.z * transform.forward.z > 0)
        {
            rigidbody.velocity -= new Vector3(deAccelSpeed * transform.forward.x, 0, deAccelSpeed * transform.forward.z) * Time.deltaTime;
        }
        if (rigidbody.velocity.x * transform.forward.x < 0 || rigidbody.velocity.z * transform.forward.z < 0)
        {
            rigidbody.velocity = Vector3.zero;
        }

        //}
    }

    //accel
    public void accel()
    {
        if (rigidbody.velocity.x * transform.forward.x < speed || rigidbody.velocity.z * transform.forward.z < speed)
        {
            rigidbody.velocity += new Vector3(accelSpeed * transform.forward.x, 0, accelSpeed * transform.forward.z) * Time.deltaTime;
        }
    }

    //field in front of car
    public void OnTriggerStay(Collider col)
    {
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            //Debug.Log(rigidbody.velocity.magnitude);
            deAccel();
        }
    }

    //If a car is in front of this car, it will deAccel
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            insideCarField = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            insideCarField = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            //Player dies...
        }
    }

	public IEnumerator turnLeft(Vector3 start, Vector3 end){
        //find the radius:
        Vector3 radVect = end - start;
        //assuming the x and z values are the same:
        if (radVect.x != radVect.z)
            Debug.Log("This is not supposed to happen...");
        
        float radius = Mathf.Abs(radVect.x);
        Debug.Log(radius);

        //current rotation
		float r = 0;
		while(r < 90){
            //rotation = 90 degrees * velocity * 4(quarter circle) * deltaTime / (2*PI*radius)
			r += 90 * (rigidbody.velocity.magnitude) * 2 * Time.deltaTime/(Mathf.PI*radius);
            
			transform.rotation = Quaternion.Euler(Vector3.up * -r);

            //change the velocity direction to the forward vector of the car
			rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
			yield return null;
		}
        //to make sure it doesn't go over 90
		r = 90;
		transform.rotation = Quaternion.Euler(Vector3.up * -r);
		rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;


		yield return null;
	}

	public IEnumerator turnRight(Vector3 start, Vector3 end){
		//find the radius:
		Vector3 radVect = end - start;
		//assuming the x and z values are the same:
		if (Mathf.Abs(radVect.x) != Mathf.Abs(radVect.z))
			Debug.Log("This is not supposed to happen...");
		
		float radius = Mathf.Abs(radVect.x);
		Debug.Log(radius);

        float startRot = transform.rotation.eulerAngles.y;

		//current rotation
		float r = startRot;
		while(r < (90 + startRot)){
			//rotation = 90 degrees * velocity * 4(quarter circle) * deltaTime / (2*PI*radius)
			r += 90 * (rigidbody.velocity.magnitude) * 2 * Time.deltaTime/(Mathf.PI*radius);
            Debug.Log(insideBox + ", " + insideCarField);
			transform.rotation = Quaternion.Euler(Vector3.up * r);
			
			//change the velocity direction to the forward vector of the car
			rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
			yield return null;
		}
		//to make sure it doesn't go over 90 + startRot
		r = 90 + startRot;
		transform.rotation = Quaternion.Euler(Vector3.up * r);
		rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
		
		
		yield return null;
	}
}
