using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int coins;

    public static int score;
    public PlayerHealth playerHealth;
    void Start()
    {
        // resets the score to 0 at the start of the game
        score = 0;

    }

    void Update()
    {
        if (coins == 10)
        {
            coins=0; 
            playerHealth.currentHealth++;
            Debug.Log("player health increase");
        }
    }
}