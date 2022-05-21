using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private PointSystem pointSystem;
    // Start is called before the first frame update
    void Start()
    {
        pointSystem = FindObjectOfType<PointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           pointSystem.AddPoints((int)(other.impulse.magnitude*other.impulse.magnitude));
        }
    }
}
