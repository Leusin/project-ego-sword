using System.Collections.Generic;
using UnityEngine;

public class CharacterContrl : MonoBehaviour
{
    [Header("Setup")]
    public Animator skinedMeshAnimator;
    public GameObject ColliderEdgePrefab;

    public List<Collider> ragdollParts = new List<Collider>();
    public List<Collider> collidingParts = new List<Collider>();
}
