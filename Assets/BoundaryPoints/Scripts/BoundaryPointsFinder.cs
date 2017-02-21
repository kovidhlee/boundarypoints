using UnityEngine;

public class BoundaryPointsFinder : MonoBehaviour
{
    public GameObject prefab;
    public MeshFilter target;

    private Vector3[] BoundaryPoints(Bounds bounds)
    {
        var C = bounds.center;
        var E = bounds.extents;
        return new Vector3[8] {
            C + new Vector3(+E.x, +E.y, +E.z),
            C + new Vector3(+E.x, +E.y, -E.z),
            C + new Vector3(+E.x, -E.y, +E.z),
            C + new Vector3(+E.x, -E.y, -E.z),
            C + new Vector3(-E.x, +E.y, +E.z),
            C + new Vector3(-E.x, +E.y, -E.z),
            C + new Vector3(-E.x, -E.y, +E.z),
            C + new Vector3(-E.x, -E.y, -E.z)
        };
    }

    public void FindPoints()
    {
        var bounds = target.sharedMesh.bounds;
        foreach (var p in BoundaryPoints(bounds))
            Detach(CreateLocalPoint(p));
    }

    private void Detach(GameObject obj)
    {
        obj.transform.SetParent(null);
    }

    private GameObject CreateLocalPoint(Vector3 localPosition)
    {
        var instance = Instantiate(prefab);
        instance.transform.SetParent(target.transform);
        instance.transform.localPosition = localPosition;
        return instance;
    }
}