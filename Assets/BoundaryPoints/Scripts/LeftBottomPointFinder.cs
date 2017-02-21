using UnityEngine;

public class LeftBottomPointFinder : MonoBehaviour
{
    public GameObject markerPrefab;
    public MeshFilter target;

    private static int Vector3LexicalCompare(Vector3 a, Vector3 b)
    {
        var delta = a - b;
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
        Vector3 leftBottom = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        var bounds = target.sharedMesh.bounds;
        var localToWorld = target.transform.localToWorldMatrix;
        foreach (var localPos in bounds.GetCorners())
        {
            var worldPos = localToWorld.MultiplyPoint(localPos);
            if (Vector3LexicalCompare(worldPos, leftBottom) < 0)
                leftBottom = worldPos;
        }

        CreateMarker(leftBottom);
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}