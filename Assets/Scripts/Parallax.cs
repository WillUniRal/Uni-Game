using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    private class Background
    {
        private float xBound;
        private float yBound;

        public float speed;
        public GameObject bg;

        private Vector3 camOffset  = Vector3.zero;
        public void SetBounds()
        {
            Sprite sprite = bg.GetComponent<SpriteRenderer>().sprite;
            xBound = sprite.texture.width / sprite.pixelsPerUnit;
            yBound = sprite.texture.height / sprite.pixelsPerUnit;
        }
        public void MoveBg(Transform parent)
        {
            //Update the background position for both the X and Y axes\\
            bg.transform.position = new Vector3(
                (parent.position.x * speed) + camOffset.x,
                (parent.position.y * speed) + camOffset.y,
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
    [SerializeField] private Background[] bgs = new Background[1];
    void Update() { foreach(Background bg in bgs) bg.MoveBg(transform);}
    void Start()  { foreach (Background bg in bgs) bg.SetBounds();}
}