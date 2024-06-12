using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    void Start()
    {
        currentHealth=maxHealth;
    }
    public void TakeDamage(float querriedDamage)
    {
        currentHealth-=querriedDamage;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0)
        {
            Debug.Log("player has died");

        }
        if (currentHealth>maxHealth)
        {
            currentHealth=maxHealth;
        }
    }
}
