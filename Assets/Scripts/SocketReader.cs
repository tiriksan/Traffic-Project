using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.IO;

public class SocketReader : MonoBehaviour
{

    public string host;
    
  //  public Byte[] ip;
    public int port;



    TcpClient socket;
    NetworkStream stream;
    StreamReader reader;

    Socket socket2;

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
            if (socketReady)
                StartCoroutine(readFromSocket());
        }
        //Debug.Log(host + ", " + port);

    }


    IEnumerator readFromSocket()
    {
        byte[] bytes = new Byte[1024];
        while (true)
        {
            Debug.Log("Pre stream.DataAvaliable");
            //Debug.Log("Data availiable: " + stream.DataAvailable + "");
            
            try
            {
                Debug.Log("here???");
                //read a line that contains the new vertex
                string read = reader.ReadLine();
               /* Debug.Log(read);
                 
                bytes = new byte[1024];
                int recieve = socket2.Receive(bytes);
                */

//                string read = Encoding.ASCII.GetString(bytes, 0 ,recieve);
                //each vertex is split with a space
                string[] allPoints = read.Split('\t');

                
                //create a vertex object from the string array
                for (int i = 0; i < 2; i++)
                {
                    string[] split = allPoints[i].Split(' ');

                    //If the points array is not instantiated yet
                    if (points == null)
                        points = new Vector3[split.Length];
                    if (prePoints == null) 
                        prePoints = new Vector3[split.Length];


                    if (pointNr > 1) 
                        prePoints[i] = new Vector3(points[0].x, 0, points[0].z);
                    points[i] = new Vector3(float.Parse(split[0]), 0, float.Parse(split[2]));
                }
                //Notifies the playerController that a new vertex has arrived
                pointNr++;

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            yield return new WaitForEndOfFrame();
        }



        yield return null;
    }

    bool setupSocket()
    {
        try
        {
     /*       IPHostEntry ipHostInfo = Dns.Resolve(host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            socket2 = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket2.Connect(remoteEP);*/


            socket = new TcpClient(host, port);
            socket.Connect(host, port);
            stream = socket.GetStream();
            reader = new StreamReader(stream);
            stream.ReadTimeout = 2000000;
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
        StopCoroutine(readFromSocket());
        closeSocket();
    }

    void closeSocket()
    {
        socket2.Close();
        if ((socketReady) && (socket.Connected))
        {
           
            reader.Close();
            socket.Close();
                          
            socketReady = false;
        }
    }
}
