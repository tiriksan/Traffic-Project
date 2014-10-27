using UnityEngine;
using System.Collections;

public class turnScript : MonoBehaviour {
	public Transform start;
	public Transform end;


	// Use this for initialization
	void Start () {
		Debug.Log("START!!!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col){
		Debug.Log("ENTER");
		if (col.tag == "Car" && !col.isTrigger)
		{
		
		StartCoroutine(col.GetComponent<CarAI>().turnLeft(start,end));
		}
	}

	void OnTriggerExit(Collider col){
		Debug.Log("EXIT");


	}
}
