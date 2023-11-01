using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private float speed;
    private Vector3 camOffset = Vector3.zero;
    private float xBound = 128f / 16f;
    private float yBound = 64f / 16f;

    void Update()
    {
        //Update the background position for both the X and Y axes\\
        bg.transform.position = new Vector3(
            (transform.position.x * speed) + camOffset.x,
            (transform.position.y * speed) + camOffset.y,
            bg.transform.position.z
        );
        //Check if the background has gone beyond the x and y boundaries\\
        float camBgOffsetX = bg.transform.localPosition.x;
        float camBgOffsetY = bg.transform.localPosition.y;
        //Is the camera at the xBounds? if so move the bg so that the camera doesnt see the edge 
        if (xBound <= Mathf.Abs(camBgOffsetX))
            camOffset.x -= camBgOffsetX;
        //Is the camera at the yBounds? if so move the bg so that the camera doesnt see the edge 
        if (yBound <= Mathf.Abs(camBgOffsetY))
            camOffset.y -= camBgOffsetY;
    }
}