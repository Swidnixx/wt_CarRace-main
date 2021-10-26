using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupportScript : MonoBehaviour
{

    Rigidbody rb;
    float lastTimeOk;


    //
    public float antiRoll = 5000;
    [Header("0 - lewe, 1- prawe")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] backWheels = new WheelCollider[2];

    void HoldWheelOnGround(WheelCollider[] wheels)
    {
        WheelHit hit;
        float leftRiding = 1;
        float rightRiding = 1;

        bool groundedL = wheels[0].GetGroundHit(out hit);
        if (groundedL) leftRiding = (-wheels[0].transform.InverseTransformPoint(hit.point).y -
                 wheels[0].radius) / wheels[0].suspensionDistance;

        bool groundedR = wheels[1].GetGroundHit(out hit);
        if (groundedL) leftRiding = (-wheels[1].transform.InverseTransformPoint(hit.point).y -
                 wheels[1].radius) / wheels[1].suspensionDistance;

        float antiRollForce = (leftRiding - rightRiding) * antiRoll;

        if (groundedL) rb.AddForceAtPosition(wheels[0].transform.up * -antiRollForce,
             wheels[0].transform.position);

        if (groundedR) rb.AddForceAtPosition(wheels[1].transform.up * antiRollForce,
     wheels[1].transform.position);
    }

    void FixedUpdate()
    {
        //HoldWheelOnGround(frontWheels);
        //HoldWheelOnGround(backWheels);
    }

  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || rb.velocity.magnitude > 1)
            lastTimeOk = Time.time;
        if (Time.time > lastTimeOk + 3)
            TurnCarBack();
    }
    void TurnCarBack()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}
