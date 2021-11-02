using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 5;

    CheckpointController[] cars;

    void Start()
    {
        
        InvokeRepeating(nameof(CountDown), 3, 1);

        GameObject[] carObjects = GameObject.FindGameObjectsWithTag("Car");
        cars = new CheckpointController[carObjects.Length];

        for(int i =0; i<cars.Length; i++)
        {
            cars[i] = carObjects[i].GetComponent<CheckpointController>();
        }
    }

    void LateUpdate()
    {
        int finishers = 0;
        foreach(CheckpointController c in cars)
        {
            if (c.lap == totalLaps + 1)
                finishers++;
        }
        if (finishers >= cars.Length && racePending)
        {
            print("Race Finished!");
            racePending = false;

        }

    }

    void CountDown()
    {
        if(timer != 0)
        {
            print("Rozpoczêcie wyœcigu za: " + timer);
            timer--;
        }
        else
        {
            print("Start!");
            racePending = true;
            CancelInvoke("CountDown");
        }
    }

    // Start is called before the first frame update

}
