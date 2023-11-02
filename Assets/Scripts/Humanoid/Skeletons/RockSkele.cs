using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockSkele : Enemy
{
    
    public float timeLeft = 3.0f;
    int count = 0;

    // Update is called once per frame
    private void Update() { 
        EnemyUpdate();

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Pew();
            timeLeft = 2.0f;
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
        Vector3 bulletVelocity = new Vector3(direction * 10, 0, 0);
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
    }
}
