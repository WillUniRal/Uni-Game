using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPoints
{
    public Transform wayPointPosition;
    public float speed;
    public float waitTime;

    private Vector3 defaultPos;

    [HideInInspector]
    public WayPoints(Vector3 defaultt, float spd, float wait)
    {
        defaultPos = defaultt;
        speed = spd;
        waitTime = wait;
    }
    [HideInInspector]
    public WayPoints() { }
    [HideInInspector]
    public Vector3 pos()
    {
        try
        {
            return wayPointPosition.position;
        }
        catch (System.NullReferenceException)
        {
            return defaultPos;
        }
    }

}