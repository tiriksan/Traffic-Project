using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform rightCamera;

    //socket variables
    public string ip;
    public int port;
    
    public float x_z_factor = 1;
   // public float y_factor;


    public float speed = 1;

    //SocketMove
    public bool useSocketReader;
    public SocketReader socketReader;
    int currPointNr;
    Vector3 deltaPosition;
    Vector3 preMovePos;


    //KeyboardMove
    public float mouseSense = 1;

    float leftRight;
    float upDown;

    float mLeftRight;
  
    public bool canMove = true;


    // Use this for initialization
    void Start()
    {
        deltaPosition = Vector3.zero;
        preMovePos = transform.position;
        if (useSocketReader)
        {
            socketReader.useSocketReader = true;
            //socketReader = gameObject.AddComponent<SocketReader>();
            //socketReader.host = ip;
            //socketReader.port = port;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useSocketReader && socketReader.socketReady)
            SocketMove();
        
        else
            KeyboardMove();
    }

    void SocketMove()
    {
        if (socketReader.pointNr > currPointNr)
        {
            preMovePos = rigidbody.position;
            deltaPosition = socketReader.points[0/*TODO: if more body parts*/] - socketReader.prePoints[0];
            deltaPosition.y = 0;
            Debug.Log(deltaPosition);
            currPointNr = socketReader.pointNr;
        }
        Debug.Log(deltaPosition);
        if (rigidbody.position.magnitude < preMovePos.magnitude + deltaPosition.magnitude)
        {
            rigidbody.MovePosition(transform.position + deltaPosition.normalized * speed * Time.deltaTime);
            if (rigidbody.position.magnitude > preMovePos.magnitude + deltaPosition.magnitude)
            {
                rigidbody.MovePosition(preMovePos + deltaPosition);
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
      
            //rotate left/right           
            rigidbody.MoveRotation(Quaternion.Euler((rigidbody.rotation.eulerAngles + mouseSense * mLeftRight * Vector3.up)));

            //move the rigidbody
            Vector3 fwd = new Vector3(rightCamera.forward.x, 0, rightCamera.forward.z);
            Vector3 right = new Vector3(rightCamera.right.x, 0, rightCamera.right.z);
            rigidbody.MovePosition(rigidbody.position + (fwd * upDown + leftRight * right).normalized * speed * Time.deltaTime);

        }
    }
}
