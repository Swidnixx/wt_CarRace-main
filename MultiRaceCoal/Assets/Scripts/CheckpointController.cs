using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int lastCheckpoint = -1;
    int checkpointCount;

    int nextCheckpoint;

    public GameObject lastPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = points.Length;

        foreach(GameObject obj in points)
        {
            if(obj.name == "0")
            {
                lastPoint = obj;
                break;
            }
        }




    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int currentCheckpoint = int.Parse(other.gameObject.name);
            if(currentCheckpoint == nextCheckpoint)
            {
                lastCheckpoint = currentCheckpoint;
                lastPoint = other.gameObject;
                if(lastCheckpoint == 0)
                {
                    lap++;
                    print($"Lap: {lap}");
                }

                nextCheckpoint++;
                nextCheckpoint = nextCheckpoint % checkpointCount;
            }
        }
    }
}
