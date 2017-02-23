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
        var colliders = GetComponentsInChildren<Collider>();
        var bigBounds = BoxCollider.bounds;
        foreach (var c in colliders)
        {
            bigBounds.Encapsulate(c.bounds);
        }
        var worldToLocal = transform.worldToLocalMatrix;
        BoxCollider.center = worldToLocal.MultiplyPoint(bigBounds.center);
        BoxCollider.size = worldToLocal.MultiplyVector(bigBounds.size);
    }

    public void Reset()
    {
        BoxCollider.center = Vector3.zero;
        BoxCollider.size = Vector3.one;
    }
}