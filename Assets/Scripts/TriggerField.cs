using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerField : MonoBehaviour {
    public List<GameObject> cars;
	// Use this for initialization
	void Start () {
	    cars = new List<GameObject>();
	}

    void OnTriggerEnter (Collider col)
    {
        if(col.tag == "Car" && !col.isTrigger)
        {
            cars.Add(col.gameObject);
            Debug.Log("Added car: " + col.gameObject);
        }
    }
    void OnTriggerExit (Collider col)
    {
        if(col.tag == "Car" && !col.isTrigger)
        {
            cars.Remove(col.gameObject);
            Debug.Log("Removed car: " + col.gameObject);
        }
    }
}
