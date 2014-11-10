using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	public Transform stats;
	public Transform screen;

	public static bool dead;
	public static bool victory;
	private static int deaths = 0;
	private int score = 0;

	private string death_msg = "You're injured!\nyou've been\n hit " + deaths
		+ " times\n wait to respawn..";
	private string victory_msg = "Congratulations";

	public AudioSource[] audio;
	private bool playSound;
	AudioClip crashSound;

	//public static Transform[] allChildren = GetComponentsInChildren<Transform>();

	void Awake(){
		Debug.Log("start " + deaths);
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.enabled = false;

		if(dead){
			if(!playSound){
				audio[0].Play();
				playSound = true;
				deaths++;
		}
			this.renderer.enabled = true;
			screen.gameObject.SetActive(true);
			this.GetComponent<TextMesh>().color = Color.red;
			this.GetComponent<TextMesh>().text = death_msg;

		}
		if(victory){
			if(!playSound){
				audio[1].Play();
				playSound = true;
				score++;
			}
			this.renderer.enabled = true;
			screen.gameObject.SetActive(true);
			this.GetComponent<TextMesh>().color = Color.green;
			this.GetComponent<TextMesh().text = victory_msg;
		}

		Invoke("Respawn",5);
	}

	void Respawn(){
			dead = false;
			playSound = false;
			Application.LoadLevel(0);
	}








}
