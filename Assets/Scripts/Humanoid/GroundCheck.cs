using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;
    //this code is pretty self explanatory, if an object in the ground check trigger zone, you are grounded. Ignore the player.
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "TZone") return;
        isGrounded = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "TZone") return;
        isGrounded = false;
    }
}
