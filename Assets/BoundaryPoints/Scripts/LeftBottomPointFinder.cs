using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LeftBottomPointFinder : MonoBehaviour
{
    public GameObject markerPrefab;
    public CubeCorner WhichCorner = CubeCorner.RightTopFar;

    public MeshFilter target
    {
        get { return GetComponent<MeshFilter>(); }
    }

    private void Start()
    {
        MarkLeftBottomPoint();
    }

    private void MarkLeftBottomPoint()
    {
        var positions = new List<Vector3>();
        var bounds = target.sharedMesh.bounds;
        var localToWorld = target.transform.localToWorldMatrix;
        foreach (var localPos in bounds.GetCorners())
        {
            var worldPos = localToWorld.MultiplyPoint(localPos);
            positions.Add(worldPos);
        }
        CreateMarker(positions[(int)WhichCorner]);
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}