using UnityEngine;
using System.Collections;

public class TrafficLights : MonoBehaviour
{

    public bool carLight;
    public bool pedestrianLight;
    public float timer; // seconds from startup
    public float offsetTime; //start light
    private bool offsetDone;
    public float durationTime; //
    public float interval; //time from red to green 

    public Transform triggerField;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        timer += Time.deltaTime;
        if (timer > offsetTime)
        {
            if (!offsetDone)
            {
                triggerField.GetComponent<TriggerField>().isActive = false;
                offsetDone = true;
            } else
            {
                checkLight();
            }
        }

    }

    public void checkLight()
    {

        if ((timer - offsetTime) % (interval + durationTime) < durationTime)
        {
            triggerField.GetComponent<TriggerField>().isActive = false;
        } else
        {
            triggerField.GetComponent<TriggerField>().isActive = true;
        }
    }




}
