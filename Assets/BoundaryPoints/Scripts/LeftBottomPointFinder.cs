using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LeftBottomPointFinder : MonoBehaviour
{
    private interface IStrategy
    {
        Bounds GetBounds();

        Vector3 ToWorldPosition(Vector3 pos);
    }

    private abstract class Strategy : IStrategy
    {
        private GameObject _gameObject;

        public Strategy(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        protected T GetComponent<T>()
        {
            return _gameObject.GetComponent<T>();
        }

        protected Transform transform
        {
            get { return _gameObject.transform; }
        }

        public abstract Bounds GetBounds();

        public abstract Vector3 ToWorldPosition(Vector3 pos);
    }

    private class AccessMesh : Strategy
    {
        public AccessMesh(GameObject gameObject) : base(gameObject)
        {
        }

        public override Bounds GetBounds()
        {
            return GetComponent<MeshFilter>().sharedMesh.bounds;
        }

        public override Vector3 ToWorldPosition(Vector3 pos)
        {
            return transform.localToWorldMatrix.MultiplyPoint(pos);
        }
    }

    private class AccessCollider : Strategy
    {
        public AccessCollider(GameObject gameObject) : base(gameObject)
        {
        }

        public override Bounds GetBounds()
        {
            return GetComponent<Collider>().bounds;
        }

        public override Vector3 ToWorldPosition(Vector3 pos)
        {
            return pos;
        }
    }

    public enum BoundsSource { Mesh, Collider }

    public GameObject markerPrefab;
    public CubeCorner corner = CubeCorner.RightTopFar;
    public BoundsSource boundsSource = BoundsSource.Mesh;

    private IStrategy _strategy;

    private void Awake()
    {
        _strategy = SelectStrategy(boundsSource);
    }

    private IStrategy SelectStrategy(BoundsSource source)
    {
        switch (source)
        {
            case BoundsSource.Mesh:
                return new AccessMesh(gameObject);

            default:
                Debug.Assert(source == BoundsSource.Collider);
                return new AccessCollider(gameObject);
        }
    }

    private void Start()
    {
        MarkLeftBottomPoint();
    }

    private void MarkLeftBottomPoint()
    {
        var bounds = _strategy.GetBounds();
        var corners = bounds.GetCorners();
        var i = (int)corner;
        var pos = _strategy.ToWorldPosition(corners[i]);
        CreateMarker(pos);
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}