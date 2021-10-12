using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider wh;
    public Transform wheel_model;
    public bool frontWheel;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        Quaternion rot;

        wh.GetWorldPose(out pos, out rot);
        wheel_model.position = pos;
        wheel_model.rotation = rot;
    }
}
