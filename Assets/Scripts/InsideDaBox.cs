using UnityEngine;
using System.Collections;

public class InsideDaBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay (Collider col)
    {
        if(col.tag == "Player")
        {
            //TODO if car driving towards player

            //TODO all car objects kinda
            GameObject.Find("CC_ME_R4").GetComponent<CarAI>().deAccel();
        }
    }
}
