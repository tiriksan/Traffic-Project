using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerField : MonoBehaviour
{
    public List<GameObject> cars;
    public bool isActive;
    // Use this for initialization
    void Start()
    {
        cars = new List<GameObject>();
    }

    void Update()
    {
        if (isActive)
        {
            foreach (GameObject gobject in cars)
            {
                gobject.GetComponent<CarAI>().insideBox = true;
                gobject.GetComponent<CarAI>().deAccel();
            }
        } else
        {
            foreach (GameObject gobject in cars)
            {
                gobject.GetComponent<CarAI>().insideBox = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            cars.Add(col.gameObject);
            Debug.Log("Added car: " + col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            cars.Remove(col.gameObject);
            Debug.Log("Removed car: " + col.gameObject);
            col.gameObject.GetComponent<CarAI>().insideBox = false;
        }
    }
}
