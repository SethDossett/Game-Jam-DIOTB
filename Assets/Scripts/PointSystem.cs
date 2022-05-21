using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private float points = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddPoints(int pointsEarned)
    {
        points += pointsEarned;
       // print("Adding " + pointsEarned + " Total " + points);
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
