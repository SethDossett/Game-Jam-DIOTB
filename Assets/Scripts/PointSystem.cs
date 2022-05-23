using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] GeneralEventSO _updateScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddPoints(int pointsEarned)
    {
        points += pointsEarned;
        _updateScore.RaiseEvent();
       // print("Adding " + pointsEarned + " Total " + points);
    }
  
    public int GetPoints()
    {
        return points;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
