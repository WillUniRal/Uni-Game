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

        //simple shoot every few secounds logic
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Pew();
            timeLeft = 2.0f;
        }
    }
    void Pew()
    {
        if (Player.invis) return; //enemys cant shoot when player is invis
        float direction = Math.Sign(transform.localScale.x);
        count++;
        GameObject _bullet = Instantiate(bullet);
        _bullet.transform.position = transform.position; // needs to start at the player
        _bullet.transform.position -= new Vector3(
            -direction * 0.2f, // -direction so its infront of enemy and 0.2f is so it doesn't spawn inside enemy
            0f,
            0f
        ); 
        _bullet.name = "Bullet (" + count + ")";
        // The velo needs to be in the direction the enemy is facing.
        Vector3 bulletVelocity = new Vector3(direction * 10, 0, 0);
        //RigidBody is needed to modify velocity.
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
    }
}
