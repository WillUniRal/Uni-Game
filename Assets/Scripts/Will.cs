using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will : MonoBehaviour
{
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The will script has started");
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        Debug.Log(i);
    }
}