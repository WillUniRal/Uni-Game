using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSkele : Enemy
{
   
    // Update is called once per frame
    private new void Update() {
        base.Update(); //call the base update (Enemy)
        /*
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Pew(left);
            timeLeft = 2.0f;
        }
        */
    }
    void Pew(int direction)
    {  
        /*
        count++;
        GameObject _bullet = Instantiate(bullet, transform);
        _bullet.name = "Bullet (" + count + ")";
        Vector3 bulletVelocity = new Vector3(direction, 0, 0);
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
        */
    }
}
