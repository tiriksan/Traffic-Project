using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {

	public TriggerField triggerField;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if(col.tag == "Player"){
			triggerField.isActive = true;
		}
	}

	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			triggerField.isActive = false;
		}
	}
}
