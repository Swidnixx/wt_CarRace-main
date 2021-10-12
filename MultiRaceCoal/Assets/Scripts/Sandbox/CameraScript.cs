using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform subject;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - subject.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = subject.position + offset;
        transform.LookAt(subject);
    }
}
