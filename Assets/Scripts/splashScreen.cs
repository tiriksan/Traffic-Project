using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	public Transform stats;
	public Transform screen;

	public static bool dead;
	public static bool victory;
	private static int deaths = 0;
	private static int score = 0;

	private string death_msg;
	private string victory_msg;

	public AudioSource[] audio;
	private bool playSound;
	AudioClip crashSound;

	public static bool scenario1;	//-150,2,-15 | Uni pedestrian crossing
	public static bool scenario2;
	public static bool scenario3;	//-30,2,-15 | Main crossroad

	public static Vector3 spawnPoint1 = new Vector3(-150,2,-15);
	public static Vector3 spawnPoint2 = new Vector3(-20,2,190);
	public static Vector3 spawnPoint3 = new Vector3(-30,2,-15);

	public GameObject[] particles;

	//public static Transform[] allChildren = GetComponentsInChildren<Transform>();

	void Awake(){
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		if(scenario1){
			this.transform.parent.transform.parent.position = spawnPoint1;
			particles[0].gameObject.SetActive(true);
		}else if(scenario2){
			this.transform.parent.transform.parent.position = spawnPoint2;
			particles[1].gameObject.SetActive(true);
		}else{
			this.transform.parent.transform.parent.position = spawnPoint3;
			particles[2].gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.enabled = false;

		if(dead){
			if(!playSound){
				audio[0].Play();
				playSound = true;
				deaths++;
				death_msg = "You're injured!\nyou've been\n hit " + deaths
					+ " times\n wait to respawn..";
			}

			screen.gameObject.SetActive(true);
			this.GetComponent<TextMesh>().color = Color.red;
			this.GetComponent<TextMesh>().text = death_msg;
			this.renderer.enabled = true;

			Invoke("Respawn",5);

		}
		if(victory){
			if(!playSound){
				audio[1].Play();
				playSound = true;
				score++;
				victory_msg = "Congratulations!\ndeaths : " + deaths + "\nscore :" 
					+ score;
				//scenario1 = true;
			}

			screen.gameObject.SetActive(true);
			this.GetComponent<TextMesh>().color = Color.green;
			this.GetComponent<TextMesh>().text = victory_msg;
			this.renderer.enabled = true;

			Invoke("Respawn",5);
		}


	}

	void checkScenario(){
		if(scenario1){
			scenario1 = false;
			scenario2 = true;
			particles[2].gameObject.SetActive(false);

		}else if(scenario2){
			scenario2 = false;
			scenario3 = true;
			particles[0].gameObject.SetActive(false);
		}else{
			scenario3 = false;
			scenario1 = true;
			particles[1].gameObject.SetActive(false);
		}
	}

	void Respawn(){
		victory = false;
		dead = false;
		playSound = false;

		checkScenario();

		Application.LoadLevel(0);

	}










}
