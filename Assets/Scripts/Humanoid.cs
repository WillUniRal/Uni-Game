using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Rigidbody2D rb;
    public float speed = 3f;
    public float jumpForce = 10f;
    [HideInInspector] public GroundCheck gc;

    private bool JumpCooldown;
    private float JumpCooldownTime = 0f;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gc = gameObject.GetComponentInChildren<GroundCheck>();
    }
    public void Move(int amount) {
        //Debug.Log(speed * amount);
        rb.velocity = new (speed * amount,rb.velocity.y);
    }
    public void Jump() {
        if (!Grounded() || JumpCooldown) return;
        rb.AddForceY(jumpForce);
        JumpCooldown = true;
    }
    public void Update()
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
