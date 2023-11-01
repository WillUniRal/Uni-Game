using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPoints 
{
    public Transform wayPointPosition; //position of the destination the object will end up in
    public float speed; //how fast It will go after that pos
    public float waitTime; //how long it will wait at that pos

    private Vector3 defaultPos;
     
    public WayPoints(Vector3 defaultt, float spd, float wait)
    {
        defaultPos = defaultt;
        speed = spd;
        waitTime = wait;
    } 
    public Vector3 pos() //just looks better than typing wayPoints[i].wayPointPosition.position (wayPoints[i].pos())
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