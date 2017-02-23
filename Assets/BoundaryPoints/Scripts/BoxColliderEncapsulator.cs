using System.Linq;
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
        var colliders = GetComponentsInChildren<Collider>().ToList();
        colliders.Remove(BoxCollider);
        if (colliders.Count == 0)
        {
            Debug.Log("There is no child has collider.");
            return;
        }

        var bigBounds = colliders.First().bounds;
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