using UnityEngine;
using System.Collections;
using System.Net.Sockets;
//using System.Net;
using System.Text;
using System;
using System.IO;

public class SocketReader : MonoBehaviour
{

    public string host;

    //  public Byte[] ip;
    public int port;

    float counter = 0;

    TcpClient socket;
    NetworkStream stream;
    StreamReader reader;

    //  Socket socket2;

    public bool useSocketReader = false;
    public bool socketReady { get; private set; }

    public Vector3[] points { get; private set; }
    public Vector3[] prePoints { get; private set; }
    public int pointNr { get; private set; }


    // Use this for initialization
    void Start()
    {

        points = new Vector3[2];
        prePoints = new Vector3[2];
        /*
        if (useSocketReader)
        {
            socketReady = setupSocket();
            //  if (socketReady)
            //      StartCoroutine(readFromSocket());
        }
        //Debug.Log(host + ", " + port);
         */

    }

    void LateUpdate()
    {
        if (useSocketReader)
        {
            if (socketReady)
            {
                try
                {
                    Debug.Log("here???");
                    //read a line that contains the new vertex
                    
                    Debug.Log("Reader: " + reader + ", Stream: " + stream + ", Socket: " + socket);
                    string read = reader.ReadLine();

                    Debug.Log("Read: " + read);

                    

                    string[] allPoints = read.Split('\t');



                    if (pointNr > 1)
                    {
                        prePoints[0] = new Vector3(points[0].x, 0, points[0].z);
                        prePoints[1] = new Vector3(points[1].x, 0, points[1].z);
                    }
                    points[0] = new Vector3(float.Parse(allPoints[2]), 0, float.Parse(allPoints[0]));
                    points[1] = new Vector3(float.Parse(allPoints[5]), 0, float.Parse(allPoints[3]));
                    if (points[0].x != 0 && points[0].z != 0 && points[1].x != 0 && points[1].z != 0)
                        pointNr++;
                    Debug.Log(pointNr);
                    //create a vertex object from the string array
                    /* for (int i = 0; i < 2; i++)
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
                     }*/
                    //Notifies the playerController that a new vertex has arrived

                }

                  //  }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                if (counter > 5)
                {
                    //StartCoroutine(setupSocket2());
                    socketReady = setupSocket();
                }
                else
                {
                    counter += Time.deltaTime;
                }
            }
        }

        //   yield return new WaitForEndOfFrame();
    }




    /*
    IEnumerator readFromSocket()
    {
        // byte[] bytes = new Byte[1024];
        while (true)
        {
            Debug.Log("Pre stream.DataAvaliable");
            Debug.Log("Data availiable: " + stream.DataAvailable + "");

            try
            {
                Debug.Log("here???");
                //read a line that contains the new vertex
                /*if (stream.DataAvailable)
                {
                    string read = reader.ReadLine();

                    Debug.Log("Read: " + read);

                    /* Debug.Log(read);
                 
                     bytes = new byte[1024];
                     int recieve = socket2.Receive(bytes);
                     

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

          //  }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            yield return new WaitForEndOfFrame();
        }



        yield return null;
    }
    */
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

            Debug.Log("TcpClient");
            socket = new TcpClient(host, port);
            Debug.Log("Connecting");
            socket.Connect(host, port);
            Debug.Log("setupStream");
            stream = socket.GetStream();
            stream.ReadTimeout = 1;
            reader = new StreamReader(stream);

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    /*
    IEnumerator setupSocket2()
    {
        try
        {
            Debug.Log("TcpClient");
            socket = new TcpClient(host, port);
        }
        catch (Exception e)
        {
            socketReady = false;
        }
        yield return new WaitForEndOfFrame();
        try
        {
            Debug.Log("Connecting");
            socket.Connect(host, port);
        }
        catch (Exception e)
        {
            socketReady = false;
        }
        yield return new WaitForEndOfFrame();
        Debug.Log("setupStream");
        try
        {
            stream = socket.GetStream();
            stream.ReadTimeout = 1;
        }
        catch (Exception e)
        {
            socketReady = false;
        }
        yield return new WaitForEndOfFrame();
        try
        {
            reader = new StreamReader(stream);

            socketReady = true;
        }
        catch (Exception e)
        {
            socketReady = false;
        }
        yield return null;
    }
 */

    void OnApplicationExit()
    {
        //StopCoroutine(readFromSocket());
        closeSocket();
    }

    void closeSocket()
    {
        // socket2.Close();
        if ((socketReady) && (socket.Connected))
        {

            reader.Close();
            socket.Close();

            socketReady = false;
        }
    }
}
