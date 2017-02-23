using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColliderEncapsulator))]
public class ColliderEncapsulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Encapsulate Children"))
        {
            Encapsulate((ColliderEncapsulator)target);
        }
    }

    private void Encapsulate(ColliderEncapsulator encapsulator)
    {
        var parentCollider = encapsulator.BoxCollider;
        var colliders = encapsulator.GetComponentsInChildren<Collider>();
        var bigBounds = parentCollider.bounds;
        foreach (var c in colliders)
        {
            bigBounds.Encapsulate(c.bounds);
        }
        var worldToLocal = encapsulator.transform.worldToLocalMatrix;
        parentCollider.center = worldToLocal.MultiplyPoint(bigBounds.center);
        parentCollider.size = worldToLocal.MultiplyVector(bigBounds.size);
    }
}