using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftBottomPointFinder : MonoBehaviour
{
    public GameObject markerPrefab;
    public MeshFilter target;
    public Vector3 ComparisonBasis = Vector3.one;

    private int Vector3LexicalCompare(Vector3 a, Vector3 b)
    {
        var delta = a - b;
        delta.Scale(ComparisonBasis);

        for (int i = 0; i < 3; i++)
        {
            if (delta[i] < 0)
                return -1;
            if (delta[i] > 0)
                return 1;
        }
        return 0;
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
        positions.Sort(Vector3LexicalCompare);
        CreateMarker(positions.First());
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}