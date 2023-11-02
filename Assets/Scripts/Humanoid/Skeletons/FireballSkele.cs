using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireballSkele : Enemy
{
    int count = 0;
    private float shootingTime = 5f; // Total time for shooting
    private float shootInterval = 1f; // Interval between shots
    private float cooldownTime = 5f; // Cooldown time
    private float shootingTimer = 0f;
    private float intervalTimer = 0f;
    private float cooldownTimer = 0f;
    private bool isShooting = false;

    private void Update()
    {
        EnemyUpdate();

        if (!isShooting)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                isShooting = true;
                shootingTimer = shootingTime;
            }
        }
        else
        {
            shootingTimer -= Time.deltaTime;

            if (shootingTimer > 0)
            {
                intervalTimer -= Time.deltaTime;

                if (intervalTimer <= 0)
                {
                    Pew();
                    intervalTimer = shootInterval;
                }
            }
            else
            {
                isShooting = false;
                cooldownTimer = cooldownTime;
            }
        }
    }

    void Pew()
    {
        if (Player.invis) return;
        float direction = Math.Sign(transform.localScale.x);
        count++;
        GameObject _bullet = Instantiate(bullet);
        _bullet.transform.position = transform.position;
        _bullet.transform.position -= new Vector3(
            -direction * 0.2f,
            0f,
            0f
        );
        _bullet.name = "Bullet (" + count + ")";
        Vector3 bulletVelocity = new Vector3(direction * 20, 5, 0);
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
    }
}
