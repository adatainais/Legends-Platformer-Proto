using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    [Tooltip("The particles that appear after the player collects a coin.")]
    public GameObject coinParticlesPrefab;

    private PlayerMovement playerMovementScript;

    private void Start()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovementScript == null)
        {
            Debug.LogError("PlayerMovement script not found on the player.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovementScript != null)
            {
                playerMovementScript.soundManager.PlayCoinSound();
                ScoreManager.score += 10;
                ScoreManager.coins += 1;
                
                if (coinParticlesPrefab != null)
                {
                    GameObject particles = Instantiate(coinParticlesPrefab, transform.position, Quaternion.identity);
                    Destroy(particles, 2f); // Adjust the time as per the duration of your particle effect
                }
                
                Destroy(gameObject);
            }
        }
    }
}