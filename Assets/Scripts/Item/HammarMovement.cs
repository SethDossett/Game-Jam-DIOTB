using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammarMovement : MonoBehaviour
{
    public float turnSpeed;
    private Rigidbody rb;
    private Health _health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = float.PositiveInfinity;
        _health = FindObjectOfType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(transform.forward * Mathf.Sin(Time.time*.4f) * turnSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Taken damage from hammar " + (other.impulse.magnitude * other.impulse.magnitude));
            _health.TakeDamage((int)(other.impulse.magnitude * other.impulse.magnitude));
        }

    }
}
