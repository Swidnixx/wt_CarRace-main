using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 5;

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
    void Start()
    {
        
        InvokeRepeating(nameof(CountDown), 3, 1);
    }

}
