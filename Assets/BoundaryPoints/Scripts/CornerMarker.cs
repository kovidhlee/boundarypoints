using UnityEngine;

public class CornerMarker : MonoBehaviour
{
    public GameObject markerPrefab;
    public MeshFilter target;

    public void CreateMarkers()
    {
        var bounds = target.sharedMesh.bounds;
        foreach (var p in bounds.GetCorners())
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