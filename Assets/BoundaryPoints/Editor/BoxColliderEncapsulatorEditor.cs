using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxColliderEncapsulator))]
public class BoxColliderEncapsulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Encapsulate Children"))
        {
            ((BoxColliderEncapsulator)target).Encapsulate();
        }
    }
}