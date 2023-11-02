using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Humanoid
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform poslol;
    [SerializeField] private Gun gun; // Reference to the player's gun

    private Vector3 targetPos;

    [SerializeField] private float speedBoostMultiplier = 2f; // Speed increase multiplier
    private float speedBoostDuration = 0.0f; // Duration of the speed boost
    private GameObject speedPotion; // Reference to the Speed Potion object
    private float baseSpeed;
    private float multiplier;
    [SerializeField] private float camSpeed;

    int input()
    {
        return Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
    }
    private GameObject deathstatus;
    private new void Start()
    {
        base.Start();
        deathstatus = cam.transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    private void Update()
    {
        HumanoidUpdate();
        Move(input());
        if (CheckForASCIIKey(' ')) Jump();


        // Handle shooting with the trigger button (R key)
        if (Input.GetKeyDown(KeyCode.R) && gun != null)
        {
            Shoot();
        }

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

    // Modify the TakeDamage method to update player HP
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
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
        deathstatus.SetActive(true);
        
        // You can add more game over logic here, such as restarting the level or ending the game.
    }

    // Method to pick up the gun
    private void PickUpGun()
    {
        gun.gameObject.SetActive(true); // Activate the gun
        gun = null; // Remove reference to the gun (assumes you can only carry one gun)
    }

    // Method to shoot the gun
    private void Shoot()
    {
        gun.Shoot(); // Call the Shoot method in the Gun script
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
