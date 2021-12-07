using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayer;

    void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayer = gameObject; 
        }
    }
}
