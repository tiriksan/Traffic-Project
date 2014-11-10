using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider col)
    {
        GameObject.Find("Stats").GetComponent<splashScreen>().victory = true;
        Debug.Log("adsfjsdlfkjsalødkfjaslødkfj");
    }
    void OnTriggerStay(Collider col)
    {
        col.rigidbody.AddForce(Vector3.up*20);
    }
}
