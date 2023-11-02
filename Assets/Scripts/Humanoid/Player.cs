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
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject HealthBarBorder;
    private Vector3 targetPos;

    private float invisDuration = 0.0f;
    [HideInInspector] public static bool invis = false;

    private float baseSpeed;
    [SerializeField] private float speedBoostMultiplier = 2f; // Speed increase multiplier
    private float speedBoostDuration = 0.0f; // Duration of the speed boost

    private float baseJumpForce;
    [SerializeField] private float jumpBoostMultiplier = 2f;
    private float jumpBoostDuration = 0.0f;

    [SerializeField] private int regenHpAmount = 5;
    private float regenDuration = 0.0f;

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
        SpeedBoost();
        JumpBoost();
        Regen();
        Invis();
        HealthBar.transform.localScale = new Vector3 (Math.Max(0f,Mathf.Lerp(HealthBar.transform.localScale.x, HP/100f,Time.deltaTime*10f)),1f,1f);
    }
    private void SpeedBoost()
    {
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
    private void JumpBoost()
    {
        if (jumpBoostDuration > 0.0f)
        {
            jumpForce = baseJumpForce * jumpBoostMultiplier;
            jumpBoostDuration -= Time.deltaTime;

            if(jumpBoostDuration <= 0.0f) jumpForce = baseJumpForce; 
        }
    }
    private void Regen()
    {
        if(regenDuration > 0.0f)
        {
            float regenDelta = regenDuration;
            regenDuration -= Time.deltaTime;
            if (regenDuration % 0.5 > 0.4f && regenDelta % 0.5 < 0.1f) HP = Math.Min(regenHpAmount+HP,100);
        }
    }
    private void Invis()
    {
        if(invisDuration > 0.0f)
        {
            invisDuration -= Time.deltaTime;
            if (invisDuration <= 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                invis = false;
            }
        }
    }

    // Apply speed boost to the player
    public void ApplySpeedBoost(float duration)
    {
        baseSpeed = speed;
        speedBoostDuration = duration;
    }

    public void ApplyJumpBoost(float duration)
    {
        baseJumpForce = jumpForce;
        jumpBoostDuration = duration;
    }
    public void ApplyRegen(float duration)
    {
        regenDuration = duration;
    }
     public void ApplyInvis(float duration)
     {
        invis = true;
        invisDuration = duration;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f/ 4);
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
    public void Die()
    {
        HealthBar.SetActive(false);
        HealthBarBorder.SetActive(false);
        Debug.Log("You Died");
        deathstatus.SetActive(true);
        
        // You can add more game over logic here, such as restarting the level or ending the game.
    }

    // Method to pick up the gun
    public void PickUpGun()
    {
        gun.gameObject.SetActive(true); // Activate the gun
        gun = null; // Remove reference to the gun (assumes you can only carry one gun)
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
