using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammarMovement : MonoBehaviour
{
    public float turnSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = float.PositiveInfinity;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(transform.forward * Mathf.Sin(Time.time) * turnSpeed *Time.deltaTime);
    }
}
