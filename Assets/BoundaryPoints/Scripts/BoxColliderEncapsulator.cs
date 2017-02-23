using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class BoxColliderEncapsulator : MonoBehaviour
{
    public BoxCollider BoxCollider
    {
        get { return GetComponent<BoxCollider>(); }
    }

    public void Encapsulate()
    {
        var parentCollider = BoxCollider;
        var colliders = GetComponentsInChildren<Collider>();
        var bigBounds = parentCollider.bounds;
        foreach (var c in colliders)
        {
            bigBounds.Encapsulate(c.bounds);
        }
        var worldToLocal = transform.worldToLocalMatrix;
        parentCollider.center = worldToLocal.MultiplyPoint(bigBounds.center);
        parentCollider.size = worldToLocal.MultiplyVector(bigBounds.size);
    }
}