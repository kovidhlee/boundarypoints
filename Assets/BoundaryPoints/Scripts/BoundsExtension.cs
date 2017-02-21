using UnityEngine;

public static class BoundsExtension
{
    public static Vector3[] GetCorners(this Bounds bounds)
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
}