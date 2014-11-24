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

	public bool playSound;
    public AudioSource[] pedestrianSound;


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

        //if 1 sec untill offset is done -> change to yellow light

        if (timer < offsetTime && timer + 1 > offsetTime)
        {
            redTL.shader = diffuse;
            yellowTL.shader = emitter;
        }
        if (timer > offsetTime)
        {
            if (!offsetDone)
            {

                triggerField.GetComponent<TriggerField>().isActive = false;

                yellowTL.shader = diffuse;
                greenTL.shader = emitter;

                redPL.shader = diffuse;
                greenPL.shader = emitter;
                
				//StartCoroutine(pedestrianBeep()); 

                offsetDone = true;
            }
            else
            {
                checkLight();
            }
        }

    }

	/*IEnumerator pedestrianBeep(){
		if(!playSound){
			pedestrianSound[0].PlayDelayed(2.0f);
			playSound = true;
		}
		yield return null;
	}*/

    public void checkLight()
    {

        //timer - offset = currentTime
        //interval + durationTime -> the length of a red+green loop
        //

        if ((timer - offsetTime) % (interval + durationTime) < durationTime)
        {

            triggerField.GetComponent<TriggerField>().isActive = false;
            yellowTL.shader = diffuse;
            greenTL.shader = emitter;
            if ((timer - offsetTime) % (interval + durationTime) > durationTime * 2 / 3) //flashing green man
            {
                if ((timer - offsetTime)*50 % 40 >= 30)
                    greenPL.shader = diffuse;
                else
                    greenPL.shader = emitter;
            }
            else
            {
                redPL.shader = diffuse;
                greenPL.shader = emitter;
            }
            //TODO:
            /*
            foreach(AudioSource audio in pedestrianSound){
                audio.Play(0);
            }
             * */
        }
        else
        {
            //change from green to yellow
            if ((timer - offsetTime) % (interval + durationTime) < durationTime + 1)
            {
                yellowTL.shader = emitter;
                greenTL.shader = diffuse;
            }
            //change from red to yellow
            else if ((timer - offsetTime) % (interval + durationTime) > (durationTime + interval - 1))
            {
                redTL.shader = diffuse;
                yellowTL.shader = emitter;
            }
            //set red light
            else
            {

                redTL.shader = emitter;
                yellowTL.shader = diffuse;

                redPL.shader = emitter;
                greenPL.shader = diffuse;
            }
            triggerField.GetComponent<TriggerField>().isActive = true;
        }
    }




}
