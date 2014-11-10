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

    public bool socketReady { get; private set; }

    public Vector3[] points { get; private set; }
    public int pointNr { get; private set; }


    // Use this for initialization
    void Start()
    {
        socketReady = setupSocket();

    }

    // Update is called once per frame
    void Update()
    {
        if (stream.DataAvailable)
        {
            //read a line that contains the new vertex
            string read = reader.ReadLine();

            //each vertex is split with a space
            string[] split = read.Split(' ');

            //If the points array is not instantiated yet
            if (points == null)
                points = new Vector3[split.Length];

            //create a vertex object from the string array
            for (int i = 0; i < 3; i++)
            {
                points[0 /*TODO: if more than one marker */] = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
            }
            //Notifies the playerController that a new vertex has arrived
            pointNr++;
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
