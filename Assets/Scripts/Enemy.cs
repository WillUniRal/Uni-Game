using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Humanoid
{

    private Transform target; // The target (player, waypoint, etc.) for path following
    private float jumpCooldownTimer = 0.0f; // Cooldown timer for jumping
    private float jumpIntervalMin = 1.0f; // Minimum time interval for jumping
    private float jumpIntervalMax = 5.0f; // Maximum time interval for jumping
    private float obstacleDetectionDistance = 1.0f; // Distance to detect obstacles

    // Set the target for path following
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Override the Update method to implement enemy behavior
    protected override void Update()
    {
        base.Update(); // Call the base class Update method

        if (target != null)
        {
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
        }
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
}