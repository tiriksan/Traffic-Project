using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform rightCamera;

    //socket variables
    //    public string ip;
    //    public int port;

    public float x_z_factor = 1;
    // public float y_factor;


    public float speed = 1;

    //SocketMove
    public bool useSocketReader;
    public SocketReader socketReader;

    public bool useMotionFileReader;
    public MotionFileReader motionFileReader;

    int currPointNr;
    Vector3 deltaPosition;
    Vector3 preMovePos;
    public int numPoints = 2;
    //motionPointsOffset:
    public float movePointZOffset = 0;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (useSocketReader && socketReader.socketReady)
            SocketMove();

        else if (useMotionFileReader)
            FileMove();
        else
            KeyboardMove();
    }

    void SocketMove()
    {
        Debug.Log("Pointnrs: " + socketReader.pointNr + ", " + currPointNr);
        if (socketReader.pointNr > currPointNr)
        {
            preMovePos = rigidbody.position;
            if (currPointNr > 1)
            {
                Vector3 prePoint1 = socketReader.prePoints[0];
                Vector3 prePoint2 = socketReader.prePoints[1];

                Vector3 point1 = socketReader.points[0];
                Vector3 point2 = socketReader.points[1];

                Vector3 deltaPoints = point1 - point2;

                //Is this really necessary? idk
                transform.forward = (new Vector3(deltaPoints.z, 0, deltaPoints.x)).normalized;

                Vector3 avgPrePoint = ((prePoint1 + prePoint2) / 2) - transform.forward * movePointZOffset;
                Vector3 avgPoint = ((point1 + point2) / 2) - transform.forward * movePointZOffset;

                avgPrePoint.y = 0;
                avgPoint.y = 0;

                Vector3 deltaAvgPos = avgPoint - avgPrePoint;

                //deltaPosition = new Vector3(deltaAvgPos.x * transform.forward.x, 0, deltaAvgPos.z * transform.forward.z);
                deltaPosition = deltaAvgPos.normalized;


                Debug.Log("Delta pos: " + deltaPosition);
                
            }
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

    void FileMove()
    {
        if (motionFileReader.pointNr > currPointNr)
        {
            preMovePos = rigidbody.position;
            if (currPointNr > 1)
            {
                Vector3 prePoint1 = motionFileReader.prePoints[0];
                Vector3 prePoint2 = motionFileReader.prePoints[1];

                Vector3 point1 = motionFileReader.points[0];
                Vector3 point2 = motionFileReader.points[1];

                Vector3 deltaPoints = point1 - point2;

                //Is this really necessary? idk
                transform.forward = (new Vector3(deltaPoints.z, 0, deltaPoints.x)).normalized;

                Vector3 avgPrePoint = ((prePoint1 + prePoint2) / 2) - transform.forward * movePointZOffset;
                Vector3 avgPoint = ((point1 + point2) / 2) - transform.forward * movePointZOffset;

                avgPrePoint.y = 0;
                avgPoint.y = 0;

                Vector3 deltaAvgPos = avgPoint - avgPrePoint;

                //deltaPosition = new Vector3(deltaAvgPos.x * transform.forward.x, 0, deltaAvgPos.z * transform.forward.z);
                deltaPosition = deltaAvgPos.normalized;


                Debug.Log("Delta pos: " + deltaPosition);

            }
            currPointNr = motionFileReader.pointNr;
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
