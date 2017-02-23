using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class ColliderEncapsulator : MonoBehaviour
{
    public BoxCollider BoxCollider
    {
        get { return GetComponent<BoxCollider>(); }
    }
}