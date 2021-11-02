using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int lastCheckpoint = -1;
    int checkpointCount;

    int nextCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        checkpointCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int currentCheckpoint = int.Parse(other.gameObject.name);
            if(currentCheckpoint == nextCheckpoint)
            {
                lastCheckpoint = currentCheckpoint;
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
