using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private float speed;
    private Vector3 startPos;
    private Vector3 camOffset = Vector3.zero;
    private float xBound = 128f / 16f;
    private float yBound = 64f / 16f;

    private void Start()
    {
        startPos = bg.transform.position;
    }

    void Update()
    {
        //Update the background position for both the X and Y axes\\
        bg.transform.position = new Vector3(
            (transform.position.x * speed) + camOffset.x,
            (transform.position.y * speed) + camOffset.y,
            bg.transform.position.z
        );

        //Check if the background has gone beyond the x and y boundaries\\
        float camPosOffsetX = bg.transform.localPosition.x;
        float camPosOffsetY = bg.transform.localPosition.y;

        if (xBound <= camPosOffsetX || camPosOffsetX <= -xBound)
        {
            camOffset -= new Vector3(camPosOffsetX, 0f, 0f);
        }
        if (yBound <= camPosOffsetY || camPosOffsetY <= -yBound)
        {
            camOffset -= new Vector3(0f, camPosOffsetY, 0f);
        }
    }
}




