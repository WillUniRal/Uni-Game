using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") return;
        isGrounded = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") return;
        isGrounded = false;
    }
}
