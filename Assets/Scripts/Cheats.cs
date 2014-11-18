using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		inputCommand();
	}

	void inputCommand(){
		if(Input.GetKey("1")){
			splashScreen.scenario1 = true;
			splashScreen.scenario2 = false;
			splashScreen.scenario3 = false;
			Application.LoadLevel(0);
		}
		else if(Input.GetKey("2")){
			splashScreen.scenario1 = false;
			splashScreen.scenario2 = true;
			splashScreen.scenario3 = false;
			Application.LoadLevel(0);		
		}
		else if(Input.GetKey("3")){
			splashScreen.scenario1 = false;
			splashScreen.scenario2 = false;
			splashScreen.scenario3 = true;
			Application.LoadLevel(0);		
		}else if(Input.GetKey("escape")){
			Application.Quit();
		}
	}


}
