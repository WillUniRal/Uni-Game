using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
[CustomEditor(typeof(MovingPlatform))]
[CanEditMultipleObjects]
public class PlatformEditor : Editor
{
    // Update is called once per frame

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MovingPlatform plt = (MovingPlatform)target;

        //initiate the offset
        if (GUILayout.Button("Generate connections")) plt.InstantiateConnectors();

        //set the offset 
        if (plt.points != null && plt.points.Length == 4) plt.SetConnectors();


    }
}
#endif