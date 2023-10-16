using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 3f;
    public float jumpForce = 10f;
    void Awake() {
        Debug.Log("hi");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(int amount) {
        //Debug.Log(speed * amount);
        rb.velocity = new (speed * amount,rb.velocity.y);
    }
    public void Jump() {
        Debug.Log("Jump!");
        rb.AddForceY(jumpForce);
    }
}
