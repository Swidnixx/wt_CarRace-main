using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public Wheel[] wheels;

    public float torque = 500;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 500;
    public float maxSpeed = 200; // km/h

    public Rigidbody rb;
    public float currentSpeed;

    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;

        float appliedTorque = 0;
        currentSpeed = rb.velocity.magnitude * 3.6f; // m/s -> km/h

        if(currentSpeed < maxSpeed)
        {
            appliedTorque = accel * torque;
        }

        foreach(Wheel wheel in wheels)
        {
            wheel.wh.motorTorque = appliedTorque;

            if(wheel.frontWheel)
            {
                wheel.wh.steerAngle = steer;
            }

            if(!wheel.frontWheel)
            {
                wheel.wh.brakeTorque = brake;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");
        Drive(accel, brake, steer);
    }
}
