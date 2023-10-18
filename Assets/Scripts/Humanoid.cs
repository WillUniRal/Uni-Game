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
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gc = gameObject.GetComponentInChildren<GroundCheck>();
    }
    public void Move(int amount) {
        //Debug.Log(speed * amount);
        rb.velocity = new (speed * amount,rb.velocity.y);
    }
    public void Jump() {
        if (!Grounded()) return;
        Debug.Log("Jump!");
        rb.AddForceY(jumpForce);
    }
    public bool Grounded()
    {
        return gc.isGrounded;
    }
}
