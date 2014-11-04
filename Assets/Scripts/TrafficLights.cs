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

    //to change the lights on the objects
    public GameObject[] trafficLObjects;
    public GameObject[] pedestrianLObjects;

    public Material[] originalMats;
    Material[] TLMats;
    Material[] PLMats;
    Shader emitter;
    Shader diffuse;
    Material redTL;
    Material yellowTL;
    Material greenTL;

    Material redPL;
    Material greenPL;

    AudioSource[] pedestrianSound;


    // Use this for initialization
    void Start()
    {
        TLMats = new Material[4];
        PLMats = new Material[3];

        //set the different shaders
        emitter = Shader.Find("VertexLit");
        diffuse = Shader.Find("Diffuse");

        //traffic light materials
        redTL = new Material(originalMats[0]);
        yellowTL = new Material(originalMats[1]);
        greenTL = new Material(originalMats[2]);


        TLMats[0] = originalMats[5];
        TLMats[1] = redTL;
        TLMats[2] = yellowTL;
        TLMats[3] = greenTL;
        redTL.shader = emitter;


        //pedestrian light materials
        redPL = new Material(originalMats[3]);
        greenPL = new Material(originalMats[4]);

        PLMats[0] = originalMats[5];
        PLMats[1] = greenPL;
        PLMats[2] = redPL;
        


        redPL.shader = emitter; //start with red light

        foreach (GameObject go in trafficLObjects)
        {
            foreach (MeshRenderer mr in go.GetComponentsInChildren<MeshRenderer>())
            {
                if (mr.materials.Length == 4)
                    mr.materials = TLMats;
            }
        }

        foreach (GameObject go in pedestrianLObjects)
        {
            foreach (MeshRenderer mr in go.GetComponentsInChildren<MeshRenderer>())
            {
                if (mr.materials.Length == 3)
                    mr.materials = PLMats;
            }
        }

        //get the audiosources
        pedestrianSound = new AudioSource[pedestrianLObjects.Length];
        int index = 0;
        foreach (GameObject go in pedestrianLObjects)
        {
            pedestrianSound[index] = go.GetComponent<AudioSource>();
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //        Debug.Log(timer);
        timer += Time.deltaTime;
        if (timer > offsetTime)
        {
            if (!offsetDone)
            {

                triggerField.GetComponent<TriggerField>().isActive = false;

                redTL.shader = diffuse;
                greenTL.shader = emitter;

                redPL.shader = diffuse;
                greenPL.shader = emitter;
                
                offsetDone = true;
            }
            else
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
            redTL.shader = diffuse;
            greenTL.shader = emitter;

            redPL.shader = diffuse;
            greenPL.shader = emitter;
            foreach(AudioSource audio in pedestrianSound){
                audio.Play();
            }
        }
        else
        {
            triggerField.GetComponent<TriggerField>().isActive = true;
            redTL.shader = emitter;
            greenTL.shader = diffuse;

            redPL.shader = emitter;
            greenPL.shader = diffuse;
        }
    }




}
