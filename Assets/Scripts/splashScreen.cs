using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	public Transform stats;
	public Transform screen;
	public static bool dead;
	private static int deaths = 1;
	private int score;

	//public static Transform[] allChildren = GetComponentsInChildren<Transform>();

	void Awake(){
		Debug.Log("start " + deaths);
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {

		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.enabled = false;
		//gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;

		if(dead){

			Debug.Log("deaths : " + deaths);
			this.renderer.enabled = true;
			screen.gameObject.SetActive(true);

			this.GetComponent<TextMesh>().text = "You're injured!\nyou've been hit\n " + deaths
				+ " times\n wait to respawn..";

			Invoke("Respawn",3);
		}
	}

	void Respawn(){
		if(dead){
			dead = false;
			//Application.LoadLevelAdditive(0);
			deaths++;
			Debug.Log ("death" + deaths);
			Application.LoadLevel(0);

		}
	}








}
