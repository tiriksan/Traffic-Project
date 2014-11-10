using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log(gameObject);
            splashScreen.victory = true;
            Debug.Log("adsfjsdlfkjsalødkfjaslødkfj");
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            col.rigidbody.AddForce(Vector3.up * 50);
        }
    }
}
