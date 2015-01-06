using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class MotionFileReader : MonoBehaviour {

    public string fileName;
    public float interval;
	// Use this for initialization


    public Vector3[] points { get; private set; }
    public Vector3[] prePoints { get; private set; }
    public int pointNr { get; private set; }


	void Start () {
        points = new Vector3[2];
        prePoints = new Vector3[2];

        StartCoroutine(readFromFile());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator readFromFile()
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(fileName);
        }
        catch(IOException e)
        {
            Debug.Log(e);
        }

        while (sr != null && !sr.EndOfStream)
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
            pointNr++;
            Debug.Log(pointNr);
            yield return new WaitForSeconds(interval);
        }
        yield return null;
    }
}
