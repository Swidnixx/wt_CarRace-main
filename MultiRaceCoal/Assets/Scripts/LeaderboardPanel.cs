using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    public Text[] placesTexts;
    
    void Start()
    {
        Leaderboard.Reset();
    }

    void LateUpdate()
    {
        List<string> places = Leaderboard.GetPlaces();
        for(int i=0; i< placesTexts.Length; i++)
        {
            if(i < places.Count)
            {
                placesTexts[i].text = places[i];
            }
            else
            {
                placesTexts[i].text = "";
            }
        }
    }
}
