using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Enemy : Humanoid
{
    [SerializeField] private Transform target; // The target (player, waypoint, etc.) for path following
    private float jumpCooldownTimer = 0.0f; // Cooldown timer for jumping
    private float jumpIntervalMin = 1.0f; // Minimum time interval for jumping
    private float jumpIntervalMax = 5.0f; // Maximum time interval for jumping
    private float obstacleDetectionDistance = 1.0f; // Distance to detect obstacles
    int left = -10;
    [SerializeField] private int attackDamage = 10; // Damage dealt by the enemy (10 HP)
    [SerializeField] private float attackCooldown = 2.0f; // Cooldown between attacks (2 seconds)
    [SerializeField] private GameObject bullet;
    private float lastAttackTime = 0.0f; // Time of the last attack
    int count = 0;
    public float timeLeft = 3.0f;

    [SerializeField] private GameObject weapon; // Reference to the enemy's weapon

    // Set the target for path following
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    // Override the Update method to implement enemy behavior
    private new void Update()
    {
        Debug.Log("Updated");
        base.Update();

        if (target != null)
        {

            // Calculate direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Move left or right using the base class's "Move" method
            if (direction.x < 0)
            {
                Move(-1);
            }
            else if (direction.x > 0)
            {
                Move(1);
            }

            // Check for obstacles in front
            if (IsObstacleInFront())
            {
                Jump(); // Jump to clear the obstacle
            }

            // Handle jumping at random intervals
            HandleJumping();

            // Attack the player
            Attack();
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Debug.Log("SHOOT");
                Pew(left);
                timeLeft = 3.0f;
            }
        }
    }

    void Pew(int direction)
    {
        count++;
        GameObject _bullet = Instantiate(bullet, transform);
        _bullet.name = "Bullet (" + count + ")";
        Vector3 bulletVelocity = new Vector3(direction, 0, 0);
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
    }
    private bool IsObstacleInFront()
    {
        // Cast a ray in the forward direction to detect obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, obstacleDetectionDistance);

        // If an obstacle is detected, return true; otherwise, return false
        return hit.collider != null;
    }

    private void HandleJumping()
    {
        // Check if the jump cooldown timer has expired
        if (Time.time >= jumpCooldownTimer)
        {
            Jump(); // Perform a jump
            jumpCooldownTimer = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax); // Set a new jump cooldown timer
        }
    }

    // Implement the attack method
    private void Attack()
    {
        // Check if the cooldown time has passed since the last attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Perform the attack
            // You can add logic to instantiate or activate the enemy's weapon here
            if (weapon != null)
            {
                // Add code to activate the weapon or apply damage to the player
                // For example, weapon.SetActive(true);
                target.GetComponent<Player>().TakeDamage(attackDamage);
            }

            lastAttackTime = Time.time; // Update the last attack time
        }
    }
}

