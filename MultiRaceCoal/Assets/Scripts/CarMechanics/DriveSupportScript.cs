using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupportScript : MonoBehaviour
{

    Rigidbody rb;
    float lastTimeOk;
    // Start is called before the first frame update
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
