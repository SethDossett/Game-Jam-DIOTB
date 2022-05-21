using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    public int LavaPoints;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PointSystem>().AddPoints(LavaPoints);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
