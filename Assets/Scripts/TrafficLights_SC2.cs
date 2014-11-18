using UnityEngine;
using System.Collections;

public class TrafficLights_SC2 : MonoBehaviour {

	public TriggerField triggerField;

	public bool carLight;
	public bool pedestrianLight;

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
	void Start () {
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

	}
	
	// Update is called once per frame
	void Update () {
		if(!triggerField.isActive){
			redTL.shader = diffuse;
			greenTL.shader = emitter;
			
			redPL.shader = diffuse;
			greenPL.shader = emitter;
		}else{
			redTL.shader = emitter;
			greenTL.shader = diffuse;
			
			redPL.shader = emitter;
			greenPL.shader = diffuse;
		}
	}
}
