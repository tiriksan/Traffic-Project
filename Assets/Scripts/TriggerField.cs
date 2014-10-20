using UnityEngine;
using System.Collections;

public class TriggerField : MonoBehaviour {
    ArrayList cars;
	// Use this for initialization
	void Start () {
	    cars = new ArrayList();
	}

    void OnTriggerEnter (Collider col)
    {
        if(col.tag == "Car")
        {
            cars.Add(col.gameObject);
        }
    }
    void OnTriggerExit (Collider col)
    {
        if(col.tag == "Car")
        {
            cars.Remove(col.gameObject);
        }
    }
}
