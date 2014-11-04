using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	public Transform stats;
	public static bool dead;
	private int deaths;
	private int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(dead){
			Debug.Log("you're dead, huehue");
			this.renderer.enabled = true;
			//this.enabled = true;


		}
	}




}
