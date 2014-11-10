using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;

    //SocketMove
    public bool useSocketReader;
    SocketReader socketReader;
    int currPointNr;
    Vector3 moveTowards;


    //KeyboardMove
    public float mouseSense = 1;

    float leftRight;
    float upDown;

    float mLeftRight;
    float mUpDown;

    public bool canMove = true;
    public Transform oculus;


    // Use this for initialization
    void Start()
    {
        moveTowards = transform.position;
        if (useSocketReader)
        {
            socketReader = gameObject.AddComponent<SocketReader>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useSocketReader && socketReader.socketReady)
        {
            SocketMove();
        }
        else
        {
            KeyboardMove();
        }
    }

    void SocketMove()
    {
        if (socketReader.pointNr > currPointNr)
        {
            moveTowards = socketReader.points[0/*TODO: if more body parts*/];
            currPointNr = socketReader.pointNr;
        }
        if (rigidbody.position != moveTowards)
        {
            rigidbody.MovePosition((moveTowards - transform.position).normalized * speed * Time.deltaTime);
            if ((moveTowards - transform.position).magnitude < 1)
            {
                rigidbody.MovePosition(moveTowards);
            }
        }
    }

    void KeyboardMove()
    {
        if (canMove)
        {
            leftRight = Input.GetAxis("Horizontal");
            upDown = Input.GetAxis("Vertical");

            mLeftRight = Input.GetAxis("Mouse X");
            mUpDown = Input.GetAxis("Mouse Y");

            Debug.Log(mUpDown);
            rigidbody.MoveRotation(Quaternion.Euler((rigidbody.rotation.eulerAngles + mouseSense * mLeftRight * Vector3.up)));

            oculus.eulerAngles = oculus.eulerAngles + Vector3.right * mUpDown * mouseSense;// = Quaternion.Euler(oculus.localRotation.eulerAngles + mUpDown * Vector3.right * mouseSense);
            //Debug.Log(oculus.rotation.eulerAngles);
            rigidbody.MovePosition(rigidbody.position + (transform.forward * upDown + leftRight * transform.right).normalized * speed * Time.deltaTime);


        }

        

    }
}
