using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxColliderEncapsulator))]
public class BoxColliderEncapsulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var encapsulator = (BoxColliderEncapsulator)target;
        if (GUILayout.Button("Encapsulate Children"))
        {
            (encapsulator).Encapsulate();
        }
        if (GUILayout.Button("Reset"))
        {
            encapsulator.Reset();
        }
    }
}