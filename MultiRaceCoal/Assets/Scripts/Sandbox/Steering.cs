using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float forceAmount = 10;
    public float constant_velocity = 1;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(constant_velocity, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
