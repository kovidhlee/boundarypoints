using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LeftBottomPointFinder : MonoBehaviour
{
    private interface IStrategy
    {
        Vector3 GetMarkerPosition(int i);
    }

    private abstract class Strategy : IStrategy
    {
        private GameObject _gameObject;

        protected Strategy(GameObject gameObject)
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

        public Vector3 GetMarkerPosition(int i)
        {
            var bounds = GetBounds();
            var corners = bounds.GetCorners();
            return ToWorldPosition(corners[i]);
        }

        protected abstract Bounds GetBounds();

        protected abstract Vector3 ToWorldPosition(Vector3 pos);
    }

    private class AccessMesh : Strategy
    {
        public AccessMesh(GameObject gameObject) : base(gameObject)
        {
        }

        protected override Bounds GetBounds()
        {
            return GetComponent<MeshFilter>().sharedMesh.bounds;
        }

        protected override Vector3 ToWorldPosition(Vector3 pos)
        {
            return transform.localToWorldMatrix.MultiplyPoint(pos);
        }
    }

    private class AccessCollider : Strategy
    {
        public AccessCollider(GameObject gameObject) : base(gameObject)
        {
        }

        protected override Bounds GetBounds()
        {
            return GetComponent<Collider>().bounds;
        }

        protected override Vector3 ToWorldPosition(Vector3 pos)
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
        Vector3 pos = _strategy.GetMarkerPosition((int)corner);
        CreateMarker(pos);
    }

    private GameObject CreateMarker(Vector3 position)
    {
        var instance = Instantiate(markerPrefab);
        instance.transform.position = position;
        return instance;
    }
}