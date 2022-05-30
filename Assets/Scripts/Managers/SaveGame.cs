using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    public void Save(int points, int coins)
    {
        int totalCoins;
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
        totalCoins += coins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        int currentHighScore;
        currentHighScore = PlayerPrefs.GetInt("HighScore");

        if(points > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", points);
        }
        
    }


}
