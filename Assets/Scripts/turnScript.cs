using UnityEngine;
using System.Collections;

public class turnScript : MonoBehaviour
{
    public Transform start;
    public Transform end;


    // Use this for initialization
    void Start()
    {
        Debug.Log("START!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("ENTER");
        if (col.tag.Contains("Car") && !col.isTrigger)
        {
            if (col.tag.Contains("L"))
            {
                StartCoroutine(col.GetComponent<CarAI>().turnLeft(start.position, end.position));
            }
            if (col.tag.Contains("R"))
            {
     //           StartCoroutine(col.GetComponent<CarAI>().turnRight(start.position, end.position));
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("EXIT");


    }
}
