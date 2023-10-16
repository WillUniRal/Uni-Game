using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("no collide :(");
    }

}
