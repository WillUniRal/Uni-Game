using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Humanoid
{
    [SerializeField] private Camera cam;
    [SerializeField] private float camSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform poslol;

    private int count = 0;
    private Vector3 targetPos;

    int input()
    {
        return Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        Move(input());
        if (Input.GetKey(KeyCode.Space)) Jump();
        if (Input.GetMouseButtonDown(0)) Pew();
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
