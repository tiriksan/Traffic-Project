using UnityEngine;
using System.Collections;

public class InsideDaBox : MonoBehaviour
{

    public Transform[] carField;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnTriggerStay (Collider col)
    {
        if(col.tag == "Player")
        {
            foreach(Transform transform in carField)
            {
                foreach(GameObject gobject in transform.GetComponent<TriggerField>().cars)
                {
                    gobject.GetComponent<CarAI>().insideBox = true;
                    gobject.GetComponent<CarAI>().deAccel();
                }
            }
        }
    }

    void OnTriggerExit (Collider col)
    {
        if(col.tag == "Player")
        {
            foreach(Transform transform in carField)
            {
                foreach(GameObject gobject in transform.GetComponent<TriggerField>().cars)
                {
                    gobject.GetComponent<CarAI>().insideBox = false;
                }
            }
        }
    }
}
