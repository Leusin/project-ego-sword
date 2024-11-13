using ProjectEgoSword;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        Attack,
        ForceTransition,
        Grounded,
    }

    [Header("Setup")]
    public Animator skinedMeshAnimator;
    public GameObject ColliderEdgePrefab;

    public List<Collider> ragdollParts = new List<Collider>();
    private List<TriggerDetector> _triggerDetectors = new List<TriggerDetector>();

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

    // -----

    public List<TriggerDetector> GetAllTriggers()
    {
        if (_triggerDetectors.Count == 0)
        {
            TriggerDetector[] arr = GetComponentsInChildren<TriggerDetector>();

            foreach (TriggerDetector detector in arr)
            {
                _triggerDetectors.Add(detector);
            }
        }

        return _triggerDetectors;
    }

    public void SetRagdollParts()
    {
        ragdollParts.Clear();

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = true;
                ragdollParts.Add(c);

                if (c.GetComponent<TriggerDetector>() == null)
                {
                    c.gameObject.AddComponent<TriggerDetector>();

                }
            }
        }
    }
}
