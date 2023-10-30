using System;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class Player : Humanoid
{
    [SerializeField] private Camera cam;
    [SerializeField] private float camSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform poslol;

    private int count = 0;
    private Vector3 targetPos;

    [SerializeField] private float speedBoostMultiplier = 2f; // Speed increase multiplier
    private float speedBoostDuration = 0.0f; // Duration of the speed boost
    private GameObject speedPotion; // Reference to the Speed Potion object
    private float baseSpeed;
    private float multiplier;

    int input()
    {
        return Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        Move(input());
        if (CheckForASCIIKey(' ')) Jump();

        if (Input.GetMouseButtonDown(0)) Pew();

        // Handle speed boost
        if (speedBoostDuration > 0.0f)
        {
            // Apply speed boost
            speed = baseSpeed * speedBoostMultiplier;
            speedBoostDuration -= Time.deltaTime;

            // Check if speed boost has ended
            if (speedBoostDuration <= 0.0f)
            {
                // Restore normal speed
                speed = baseSpeed;
            }
        }
    }

    // Apply speed boost to the player
    public void ApplySpeedBoost(float duration)
    {
        baseSpeed = speed;
        speedBoostDuration = duration;
    }

    void Pew()
    {
        count++;
        GameObject _bullet = Instantiate(bullet, poslol);
        _bullet.name = "Bullet (" + count + ")";
        Vector3 bulletVelocity = new Vector3(10, 0, 0);
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
    }

    // Modify the TakeDamage method to update player HP
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }
    bool CheckForASCIIKey(int asciiValue)
    {
        if (Input.inputString.Length > 0)
        {
            char pressedKey = Input.inputString[0];
            int pressedAscii = (int)pressedKey;

            if (pressedAscii == asciiValue)
            {
                return true;
            }
        }
        return false;
    }
    // Check if the player is dead and print "You Died"
    private void Die()
    {
        Debug.Log("You Died");
        // You can add more game over logic here, such as restarting the level or ending the game.
    }

    private void FixedUpdate()
    {
        targetPos = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            cam.transform.position.z
        );

        cam.transform.position = new Vector3(
            Mathf.Lerp(cam.transform.position.x, targetPos.x, camSpeed * Time.fixedDeltaTime),
            Mathf.Lerp(cam.transform.position.y, targetPos.y, camSpeed * Time.fixedDeltaTime),
            cam.transform.position.z
        );
    }

    
}