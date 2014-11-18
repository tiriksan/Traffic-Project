using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {

	public bool isPushed;

	public TriggerField triggerField;
    public TrafficLights_SC2 TL;
	// Use this for initialization
	void Start () {
        TL = GetComponent<TrafficLights_SC2>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if(splashScreen.scenario1){
			if(col.tag == "Player"){
				triggerField.isActive = true;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(splashScreen.scenario2){
			//isPushed = true;
			if(col.tag == "Player" && !isPushed){
				StartCoroutine(PushAndWait());
			}
		}

	}

	void OnTriggerExit(Collider col){
		if(splashScreen.scenario1){
			if(col.tag == "Player"){
				triggerField.isActive = false;
			}
		}
	}

	IEnumerator PushAndWait(){
		int waitTime = Random.Range(3,10);
		isPushed = true;
		yield return new WaitForSeconds(waitTime-1);
        TL.currState = TrafficLights_SC2.state.yellow;
        yield return new WaitForSeconds(1);
        TL.currState = TrafficLights_SC2.state.green;
		Debug.Log("waitTime :" + waitTime + " " + isPushed);
		triggerField.isActive = true;
		yield return new WaitForSeconds(14);
        TL.currState = TrafficLights_SC2.state.yellow;
        yield return new WaitForSeconds(1);
        TL.currState = TrafficLights_SC2.state.red;
        triggerField.isActive = false;
			isPushed = false;
	}
}
