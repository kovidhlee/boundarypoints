using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshCorners : MonoBehaviour
{
    public float radius = 1;
    public Color color = Color.red;
    public bool drawGizmosAlways = false;

    private MeshFilter MeshFilter
    {
        get { return GetComponent<MeshFilter>(); }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmosAlways)
            OnDrawGizmosSelected();
    }

    private void OnDrawGizmosSelected()
    {
        var bounds = MeshFilter.sharedMesh.bounds;
        foreach (var c in bounds.GetCorners())
        {
            var p = MeshFilter.transform.localToWorldMatrix.MultiplyPoint(c);
            Gizmos.color = color;
            Gizmos.DrawSphere(p, radius);
        }
    }
}