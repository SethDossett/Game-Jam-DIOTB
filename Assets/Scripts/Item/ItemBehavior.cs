using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private PointSystem pointSystem;
    private Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        pointSystem = FindObjectOfType<PointSystem>();
        playerHealth = FindObjectOfType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (!playerHealth.invincible)
    //        {
    //            int hitStrength = (int) other.impulse.magnitude;
    //            pointSystem.AddPoints(hitStrength*hitStrength);
    //            playerHealth.TakeDamage(hitStrength);
    //        }
    //    }
    //}
}
