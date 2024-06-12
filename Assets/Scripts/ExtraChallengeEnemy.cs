using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExtraChallengeEnemy : MonoBehaviour
{
    public Stats enemyStats;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform[] patrolPoints;

    [Tooltip("The transform of the player.")]
    public Transform playerTransform;

    [Tooltip("The health component of the player.")]
    public PlayerHealth playerHealth;

    private int currentPatrolPoint = 1;
    private bool isAggroed = false;
    private Rigidbody rb;

    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]

        [Tooltip("How fast the enemy moves.")]
        public float speed;

        [Tooltip("Whether the enemy should move or not")]
        public bool move;

        [Tooltip("The distance within which the enemy gets aggroed by the player.")]
        public float aggroRange;
    }

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Freeze rotation if you only want to control position
    }

    void Update()
    {
        // Check distance to player and aggro if close enough
        if (!isAggroed && Vector3.Distance(transform.position, playerTransform.position) <= enemyStats.aggroRange)
        {
            isAggroed = true;
        }

        if (isAggroed)
        {
            HandleAggroBehavior();
        }
        else if (enemyStats.move)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
        Vector3 direction = (moveToPoint - transform.position).normalized;
        rb.MovePosition(transform.position + direction * enemyStats.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
        {
            currentPatrolPoint++;
            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }
    }

    void HandleAggroBehavior()
    {
        float playerHealthFraction = playerHealth.currentHealth / playerHealth.maxHealth;
        Vector3 targetPosition;
        targetPosition = playerTransform.position;

        Vector3 direction;

        if (playerHealthFraction < 0.5f)
        {
            // Move towards the player
            direction = (targetPosition - transform.position).normalized;
        }
        else
        {
            // Run away from the player
            direction = (transform.position - targetPosition).normalized;
        }

        rb.MovePosition(transform.position + direction * enemyStats.speed * Time.deltaTime);
    }
}