using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Player : Entity
{
    int input()
    {
        return Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
    }

    // Update is called once per frame
    void Update()
    { 
        Move(input());
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }
}
