using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class MotionFileReader : MonoBehaviour
{

    public string fileName;
    public float interval;
    // Use this for initialization

    
    public int numLines = 100;
    public Vector3[] points { get; private set; }
    public Vector3[] prePoints { get; private set; }
    public int pointNr { get; private set; }

    bool readerIsActive = false;

    void Start()
    {
        points = new Vector3[2];
        prePoints = new Vector3[2];

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!readerIsActive && Input.GetKeyDown(KeyCode.Space))
        {
            readerIsActive = true;
            StartCoroutine(readFromFile());
        }
    }

    IEnumerator readFromFile()
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(fileName);
        }
        catch (IOException e)
        {
            Debug.Log(e);
        }

        while (sr != null && !sr.EndOfStream && pointNr < numLines)
        {
            string read = sr.ReadLine();
            
            string[] allPoints = read.Split('\t');



            if (pointNr > 1)
            {
                prePoints[0] = new Vector3(points[0].x, 0, points[0].z);
                prePoints[1] = new Vector3(points[1].x, 0, points[1].z);
            }
            
            points[0] = new Vector3(float.Parse(allPoints[2]), 0, float.Parse(allPoints[0]));
            points[1] = new Vector3(float.Parse(allPoints[5]), 0, float.Parse(allPoints[3]));
            Debug.Log(points[0] + ", " + points[1]);

            //check if the point is valid (if none of the values is 0)
            if (points[0].x != 0 && points[0].z != 0 && points[1].x != 0 && points[1].z != 0)
                pointNr++;

            Debug.Log(pointNr);
            yield return new WaitForSeconds(interval);
        }
        sr.Close();
        readerIsActive = false;
        yield return null;
    }
}
