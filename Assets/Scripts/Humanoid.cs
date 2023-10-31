using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Humanoid : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Rigidbody2D rb;
    public float speed = 3f;
    public float jumpForce = 10f;
    [HideInInspector] public GroundCheck gc;

    public int HP = 100; // Humanoid HP with an initial value of 100

    private bool JumpCooldown;
    private float JumpCooldownTime = 0f;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gc = gameObject.GetComponentInChildren<GroundCheck>();
    }
    public void Move(int amount) {
        rb.velocity = new (speed * amount,rb.velocity.y);
        if (amount == 0) return;
        transform.localScale = new Vector3(
            startScale.x * Math.Sign(amount),
            transform.localScale.y,
            transform.localScale.z
        );
    }
    public void Jump() {
        if (!Grounded() || JumpCooldown) return;
        rb.AddForceY(jumpForce);
        JumpCooldown = true;
    }
    public Vector3 startScale;
    private void Start()
    {
        startScale = transform.localScale;
    }
    public void HumanoidUpdate()
    {
        if (!JumpCooldown) return;
        //Debug.Log(JumpCooldownTime);
        JumpCooldownTime += Time.deltaTime;
        if (JumpCooldownTime > 0.1f) {
            JumpCooldownTime = 0f;
            JumpCooldown = false;
        }
    }
    public bool Grounded()
    {
        return gc.isGrounded;
    }
}
