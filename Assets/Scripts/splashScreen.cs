using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	public Transform stats;
	public Transform screen;

	public static bool dead;
	public static bool victory;
	private static int deaths = 0;
	private static int score = 0;

	private Color colorBack = new Color(0.1f,0.1f,0.1f,0.8f); //background color screen

	private string death_msg;
	private string victory_msg;
	private string sc_1 = "Scenario 1\nWait for cars to stop\n before you can \n cross the road..";
	private string sc_2 = "Scenario 2\nStay in the zone\nwait for green man..";
	private string sc_3 = "Scenario 3\nWait for green man .." + "\n1: sc_1\n2 : sc_2\n3: sc_3\n esc: quit";

	public AudioSource[] audio; //
	private bool playSound;
	AudioClip crashSound;

	public static bool scenario1;	//-150,2,-15 | Uni pedestrian crossing
	public static bool scenario2;
	public static bool scenario3;	//-30,2,-15 | Main crossroad

	public static Vector3 spawnPoint1 = new Vector3(-150,2,-15);
	public static Vector3 spawnPoint2 = new Vector3(-20,2,175);
	public static Vector3 spawnPoint3 = new Vector3(-30,2,-15);

	public GameObject[] particles; //finish lines

	//public static Transform[] allChildren = GetComponentsInChildren<Transform>();

	void Awake(){
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		if(scenario1){
			this.transform.parent.transform.parent.transform.parent.position = spawnPoint1;
			particles[0].gameObject.SetActive(true);

		}else if(scenario2){
			this.transform.parent.transform.parent.transform.parent.position = spawnPoint2;
			particles[1].gameObject.SetActive(true);
		}else{
			this.transform.parent.transform.parent.transform.parent.position = spawnPoint3;
			particles[2].gameObject.SetActive(true);
		}
		StartCoroutine(quest());
	}
	
	// Update is called once per frame
	void Update () {
		//this.renderer.enabled = false;

		if(dead){
			if(!playSound){
				audio[0].Play();
				playSound = true;
				deaths++;
				if(deaths < 3){
					death_msg = "You're injured!\n injuries : " + deaths
						+ "\n wait to respawn..";
				}else{
					death_msg = "GAME OVER\nInjuries : " + deaths + "\nScore : " + score;
					deaths = 0;
					score = 0;
				}
			}

			screen.gameObject.renderer.material.color = colorBack;
			screen.gameObject.SetActive(true);

			this.GetComponent<TextMesh>().characterSize = 0.6f;
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

			screen.gameObject.renderer.material.color = colorBack;
			screen.gameObject.SetActive(true);

			this.GetComponent<TextMesh>().characterSize = 0.6f;
			this.GetComponent<TextMesh>().color = Color.yellow;
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

		dead = false;
		playSound = false;

		if(victory){
			checkScenario();
			victory = false;
		}


		Application.LoadLevel(0);

	}


	IEnumerator quest(){
		//sc_1 = "test test";
		float currentTime = 0;

		screen.gameObject.renderer.material.color = colorBack;
		screen.gameObject.SetActive(true);

		this.GetComponent<TextMesh>().characterSize = 0.4f;
		this.GetComponent<TextMesh>().color = Color.white;

		if(scenario1){
			this.GetComponent<TextMesh>().text = sc_1;
		}else if(scenario2){
			this.GetComponent<TextMesh>().text = sc_2;
		}else{
			this.GetComponent<TextMesh>().text = sc_3;
		}

		this.renderer.enabled = true;

		while(currentTime < 8){
			float transparency = 1 - (currentTime/8);
			Color test = screen.gameObject.renderer.material.color;
			test.a = transparency;
			screen.gameObject.renderer.material.color = test;
			currentTime += Time.deltaTime;
			yield return null;
		}
		this.renderer.enabled = false;

		yield return null;
	}


			











}
