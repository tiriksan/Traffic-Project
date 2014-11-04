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

    bool socketReady;


    // Use this for initialization
    void Start()
    {
        setupSocket();

    }

    // Update is called once per frame
    void Update()
    {
        if (stream.DataAvailable) { 
            string read  = reader.ReadLine();
            
            string[] split = read.Split(' ');
            for (int i = 0; i < 3; i++)
            {
                transform.position = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
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
