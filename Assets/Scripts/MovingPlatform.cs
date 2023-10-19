using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [System.Serializable]
    private class WayPoints
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
    [Header("Path")]
    [SerializeField] private bool startOnNatureRelic = false;
    [SerializeField] private WayPoints[] wayPoints = new WayPoints[1];

    [Header("Physics")]
    [SerializeField] private float height;
    [SerializeField] private float spring = 10f;

    [Header("Connections/connectors")]
    [SerializeField] private float connectorOffset;
    [SerializeField] private float connectionOffset;
    [SerializeField] private float connectionHeightOffset;


    [HideInInspector] public GameObject[] points;
    private Vector3[] connectors = new Vector3[2];
    private Vector3[] connections = new Vector3[2];
    private SpringJoint2D[] joints = new SpringJoint2D[2];
    private Vector3 startPos;
    private Vector3 realSPos;

    private Vector3 currentPathTarget;
    private int pathPos = 0;
    private bool wait = true;
    private float waitTime = 0;

    private void Start()
    {
        startPos = transform.position;
        realSPos = transform.position;

        if (wayPoints.Length == 0) wayPoints = new WayPoints[1];
        if (wayPoints[0].wayPointPosition == null) wayPoints[0] = new WayPoints(realSPos, wayPoints[0].speed, wayPoints[0].waitTime);

        //Debug.Log(wayPoints[0].pos());
        //Debug.Log(realSPos);
    }
    private Vector3[] CreateOffsets(float o, float y)
    {
        Vector3[] connections = {
            new(o ,y, o),
            new(-o,y, o),
            new(-o,y,-o),
            new(o ,y,-o)
        };
        return connections;
    }
    private void PathFollow()
    {
        int thisPos = (pathPos + 1) % wayPoints.Length;
        int lastPos = pathPos % wayPoints.Length;

        if (wait)
        { 
            Debug.Log(startOnNatureRelic);
            waitTime += Time.deltaTime;
            if (waitTime >= wayPoints[lastPos].waitTime)
            {
                wait = false;
                waitTime = 0;
            }
            else return;
        }

        Vector2 pathXZ = Vector2.MoveTowards(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(wayPoints[thisPos].pos().x, wayPoints[thisPos].pos().y),
            wayPoints[lastPos].speed * Time.deltaTime
        );

        startPos = new Vector3(
            pathXZ.x,
            pathXZ.y,
            Mathf.MoveTowards(startPos.z, wayPoints[thisPos].pos().z, wayPoints[lastPos].speed * Time.deltaTime)
         );
        Debug.DrawLine(startPos, wayPoints[thisPos].pos(), Color.red, Time.deltaTime);
        transform.position = new Vector3(
            startPos.x,
            startPos.y,
            transform.position.z
        );
        //if at the end of path
        if (Vector3.Distance(startPos, wayPoints[thisPos].pos()) < wayPoints[lastPos].speed * Time.deltaTime)
        {
            pathPos++;
            wait = true;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        int thisPos = (pathPos + 1) % wayPoints.Length;
        int lastPos = pathPos % wayPoints.Length;

        if (collision.gameObject.GetComponent<Rigidbody2D>() && collision.gameObject.GetComponent<Player>() && !wait)
        {
            Vector2 pathXZ = new Vector2(transform.position.x, transform.position.y) - Vector2.MoveTowards(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(wayPoints[thisPos].pos().x, wayPoints[thisPos].pos().y),
                wayPoints[lastPos].speed * Time.deltaTime
            );

            collision.transform.position += new Vector3(
                -1 * pathXZ.x,
                0f,
                0f
            );
        }

    }

    public void InstantiateConnectors()
    {
        if (!Application.isPlaying) startPos = transform.position;

        connectors = CreateOffsets(connectorOffset, transform.position.y + height);
        connections = CreateOffsets(connectionOffset, connectionHeightOffset)/*- height*/;

        points = new GameObject[2];

        while (GetComponents<SpringJoint>().Length != 0) DestroyImmediate(GetComponents<SpringJoint>()[0]);
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != null) DestroyImmediate(points[i]);

            //if (joints[i] != null) DestroyImmediate(joints[i]); 
            GameObject newObj = new GameObject();
            points[i] = newObj;

            Rigidbody2D rb = points[i].AddComponent<Rigidbody2D>();
            rb.isKinematic = true;

            joints[i] = this.gameObject.AddComponent<SpringJoint2D>();
            joints[i].autoConfigureConnectedAnchor = false;
            joints[i].connectedBody = rb;
            joints[i].distance = height;
            joints[i].anchor = connections[i];

            points[i].name = "point " + (i + 1).ToString();
            //points[i].transform.parent = transform;
            points[i].transform.position = connectors[i] + new Vector3(startPos.x, 0f, startPos.z);
        }
    }
    public void SetConnectors()
    {
        if (!Application.isPlaying) startPos = transform.position;

        connectors = CreateOffsets(connectorOffset, startPos.y + height);
        connections = CreateOffsets(connectionOffset, connectionHeightOffset)/*- height*/;

        for (int i = 0; i < points.Length; i++)
        {
            joints = GetComponents<SpringJoint2D>();
            points[i].transform.position = connectors[i] + new Vector3(startPos.x, 0f, startPos.z);

            //joints[i].maxDistance = height;
            //joints[i].minDistance = height;

            joints[i].anchor = connections[i];
            //joints[i].spring = spring;
        }
    }
    private void OnDrawGizmos()
    {
        if (points == null || points.Length != 2) return;
        SetConnectors();
        for (int i = 0; i < points.Length; i++)
        {

            int firstPoint = (i) % 2;
            int secoundPoint = (i + 1) % 2;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(points[firstPoint].transform.position, points[secoundPoint].transform.position/*, Color.cyan,0.5f*/);
            Gizmos.DrawLine(Vector3Multiply(connections[firstPoint], transform.localScale) + transform.position, Vector3Multiply(connections[secoundPoint], transform.localScale) + transform.position/*, Color.cyan, 0.5f*/);
            Gizmos.color = new Color { r = 0.988f, g = 0.55686f, b = 0.247058f, a = 1 };
            Gizmos.DrawLine(points[firstPoint].transform.position, Vector3Multiply(connections[firstPoint], transform.localScale) + transform.position/*, Color.blue, 0.5f*/);

        }
    }
    private Vector3 Vector3Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.x * b.x,
            a.y * b.y,
            a.z * b.z
        );

    }
    // Update is called once per frame
    void Update()
    {
        PathFollow();

    }
}