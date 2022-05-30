using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammarMovement : MonoBehaviour
{
    public float turnSpeed;
    private Rigidbody rb;
    private Health _health;
    public float hitMultiplyer;
    private bool hasHit;
    
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
        if (hasHit) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("HitCooldown",1f);
            hasHit = true;
            int hitStrength = (int) (other.collider.GetComponent<Rigidbody>().velocity.magnitude *hitMultiplyer);
            print("Taken damage from hammar " + hitStrength);
            _health.TakeDamage(hitStrength);
        }

    }
    public void HitCooldown()
    {
        hasHit = false;
    }
}
