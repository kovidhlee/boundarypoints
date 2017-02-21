using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftBottomPointFinder : MonoBehaviour
{
    public GameObject markerPrefab;
    public MeshFilter target;

    private static int Vector3Compare(Vector3 a, Vector3 b)
    {
        var delta = a - b;
        if (delta.x < 0)
            return -1;
        if (delta.x > 0)
            return 1;
        if (delta.y < 0)
            return -1;
        if (delta.y > 0)
            return 1;
        if (delta.z < 0)
            return -1;
        if (delta.z > 0)
            return 1;
        return 0;
    }

    private void Start()
    {
        CreateMarkers();
    }

    private void CreateMarkers()
    {
        List<Vector3> points = new List<Vector3>();

        var bounds = target.sharedMesh.bounds;
        var matrix = target.transform.localToWorldMatrix;
        foreach (var p in bounds.GetCorners())
        {
            points.Add(matrix.MultiplyPoint(p));
        }

        points.Sort(Vector3Compare);
        points.RemoveRange(1, points.Count - 1);
        var markerPosition = points.First();
        CreateMarker(markerPosition);
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}