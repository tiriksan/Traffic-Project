using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            splashScreen.victory = true;

        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            col.rigidbody.AddForce(Vector3.up * 15);
        }
    }
}
