using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Setup")]
    public Animator skinedMeshAnimator;
    public GameObject ColliderEdgePrefab;

    public List<Collider> ragdollParts = new List<Collider>();
    public List<Collider> collidingParts = new List<Collider>();

    public Rigidbody RigidbodyComponent
    {
        get
        {
            if (_cachedrigidbody == null)
            {
                _cachedrigidbody = GetComponent<Rigidbody>();
            }
            return _cachedrigidbody;
        }
    }
    private Rigidbody _cachedrigidbody;
}
