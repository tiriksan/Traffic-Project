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


        if (transform.position.x <= endRoad.position.x && -transform.position.z * transform.forward.z <= -endRoad.position.z * transform.forward.z)
        {
            transform.position = startRoad.position;
			transform.forward = startRoad.forward;
			rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
        }
//        Debug.Log("Velocity: " + rigidbody.velocity.magnitude + ", inside box: " + insideBox + ", inside car field: " + insideCarField);
        if (rigidbody.velocity.magnitude < speed && !insideBox && !insideCarField)
        {
            accel();
        }
        //rigidbody.velocity = new Vector3(-speed, 0, 0);
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
        if (col.tag == "Car" && !col.isTrigger)
        {
            //Debug.Log(rigidbody.velocity.magnitude);
            deAccel();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Car" && !col.isTrigger)
        {
            insideCarField = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Car" && !col.isTrigger)
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

	public IEnumerator turnLeft(Transform start, Transform end){
		float r = 0;
		while(r < 90){
			r += 90 * (rigidbody.velocity.magnitude) * 2 * Time.deltaTime/(Mathf.PI*25);
			transform.rotation = Quaternion.Euler(Vector3.up * -r);
			rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;
			yield return null;
		}

		r = 90;
		transform.rotation = Quaternion.Euler(Vector3.up * -r);
		rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;


		yield return null;
	}
}
