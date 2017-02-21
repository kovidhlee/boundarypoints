using UnityEngine;

public class BoundaryPointsFinder : MonoBehaviour
{
    public GameObject markerPrefab;
    public MeshFilter target;

    private Vector3[] GetCorners(Bounds bounds)
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

    public void CreateMarkers()
    {
        var bounds = target.sharedMesh.bounds;
        foreach (var p in GetCorners(bounds))
            Detach(CreateLocalMarker(p));
    }

    private void Detach(GameObject obj)
    {
        obj.transform.SetParent(null);
    }

    private GameObject CreateLocalMarker(Vector3 localPosition)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.SetParent(target.transform);
        instance.transform.localPosition = localPosition;
        return instance;
    }
}