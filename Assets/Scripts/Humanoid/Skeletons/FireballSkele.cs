using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireballSkele : Enemy
{
    int count = 0;
    private float shootingTime = 2f; // Total time for shooting
    private float shootInterval = 0.2f; // Interval between shots
    private float cooldownTime = 5f; // Cooldown time
    private float shootingTimer = 0f;
    private float intervalTimer = 0f;
    private float cooldownTimer = 0f;
    private bool isShooting = false;

    Transform shootPoint;
    Transform shootPointOffset;
    private new void Start()
    {
        base.Start();

        GameObject shootP = new GameObject();       // New GameObjects to base off of
        GameObject shootPOffset = new GameObject(); // New GameObjects to base off of

        shootP.name = "ShootPoint";
        shootPOffset.name = "ShootPointOffset";

        shootPoint = Instantiate(shootP, transform).transform;       // Make clones set the parent to this
        shootPointOffset = Instantiate(shootPOffset, shootPoint).transform; // Make clones and set the parent to shootPoint

        Destroy(shootP);       // Destroy initial
        Destroy(shootPOffset); // Destroy initial

        shootPointOffset.localPosition += new Vector3(3f, 0f, 0f);
    }
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
        //Create some code that will rotate shootPoint and then shoot the fire balls from there
        float curRot = shootPoint.localRotation.eulerAngles.z;
        shootPoint.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot +30));

        GameObject _bullet = Instantiate(bullet);

        Debug.Log(count);
        
        _bullet.transform.position = shootPointOffset.position;
        _bullet.transform.rotation = shootPoint.rotation;
        

        _bullet.name = "Bullet (" + count + ")"; 
    }
}