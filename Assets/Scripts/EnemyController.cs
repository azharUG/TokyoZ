using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pointA; // Starting point
    public Transform pointB; // Ending point
    public Transform player; // Reference to the player's transform
    public float detectionRadius = 5f; // Radius within which the enemy detects the player
    public float moveSpeed = 3f; // Movement speed of the enemy
    public float attackRange = 1f; // Range at which the enemy can attack the player

    private bool isMovingTowardsA = true; // Flag to indicate movement direction
    private bool isPlayerDetected = false; // Flag to indicate if player is detected

    private void Update()
    {
        if (isPlayerDetected)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                // Move towards the player
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                transform.position += (Vector3)directionToPlayer * moveSpeed * Time.deltaTime;
            }
            else
            {
                // Attack the player
                Debug.Log("Attacking player!");
            }
        }
        else
        {
            // Move back and forth between pointA and pointB
            if (isMovingTowardsA)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
                if (transform.position == pointA.position)
                    isMovingTowardsA = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
                if (transform.position == pointB.position)
                    isMovingTowardsA = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is within the detection radius
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius)
            {
                isPlayerDetected = true;
            }
            else
            {
                isPlayerDetected = false;
            }
        }
    }
}
