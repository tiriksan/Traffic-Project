using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.IO;

public class SocketReader : MonoBehaviour
{

    public string host;
    public int port;

    TcpClient socket;
    NetworkStream stream;
    StreamReader reader;

    public bool useSocketReader = false;
    public bool socketReady { get; private set; }

    public Vector3[] points { get; private set; }
    public Vector3[] prePoints { get; private set; }
    public int pointNr { get; private set; }


    // Use this for initialization
    void Start()
    {
        if (useSocketReader)
        {
            socketReady = setupSocket();
        }
        //Debug.Log(host + ", " + port);

    }

    // Update is called once per frame
    void Update()
    {
        if (useSocketReader)
        {
            if (socketReady)
            {
                Debug.Log(stream.DataAvailable + "");
                // Debug.Log(reader.ReadLine());
                try
                {
                    Debug.Log("here???");
                    //read a line that contains the new vertex
                    string read = reader.ReadLine();
                    Debug.Log(read);
                    //each vertex is split with a space
                    string[] split = read.Split(' ');

                    //If the points array is not instantiated yet
                    if (points == null)
                        points = new Vector3[split.Length];
                    if (prePoints == null) prePoints = new Vector3[split.Length];
                    //create a vertex object from the string array
                    for (int i = 0; i < 1; i++)
                    {
                        if (pointNr > 1) prePoints[i] = new Vector3(points[0].x, 0, points[0].z);
                        points[0 /*TODO: if more than one marker */] = new Vector3(float.Parse(split[0]), 0, float.Parse(split[2]));
                    }
                    //Notifies the playerController that a new vertex has arrived
                    pointNr++;

                }
                catch (Exception e)
                {

                }
            }
        }
    }

    bool setupSocket()
    {
        try
        {
            socket = new TcpClient(host, port);
            socket.Connect(host, port);
            stream = socket.GetStream();
            reader = new StreamReader(stream);
            stream.ReadTimeout = 1;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    void OnApplicationExit()
    {
        closeSocket();
    }

    void closeSocket()
    {
        if ((socketReady) && (socket.Connected))
        {
            reader.Close();
            socket.Close();
            socketReady = false;
        }
    }
}
