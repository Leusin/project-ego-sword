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
    public Material material;
    public GameObject ColliderEdgePrefab;

    [Header("Input")]
    public Vector2 move;
    public bool jump;
    public bool attack;

    public List<Collider> ragdollParts = new List<Collider>();
    private List<TriggerDetector> _triggerDetectors = new List<TriggerDetector>();

    public List<GameObject> bottomSpheres = new List<GameObject>();
    public List<GameObject> frontSpheres = new List<GameObject>();

    [HideInInspector] public float gravityMultiplier;
    [HideInInspector] public float pullMultiplier;

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

    public void MoveForward(float speed, float speedGraph)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * speedGraph);
    }

    public void FaceForward(bool forward)
    {
        if (forward)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public bool IsFacingForward()
    {
        if (transform.forward.z > 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

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

    public void ChangeMaterial()
    {
        if(material == null)
        {
            Debug.LogError("No material specifies");
        }

        Renderer[] arrRenderer = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in arrRenderer)
        {
            if (r.gameObject != gameObject)
            {
                r.material = material;
            }
        }
    }
    public void CreateMiddleSpheres(GameObject start, Vector3 direction, float section, int interactions, List<GameObject> spheresList)
    {
        for (int i = 0; i < interactions; i++)
        {
            Vector3 position = start.transform.position + (direction * section * (i + 1));
            GameObject newobj = CreateEdgeSphere(position);

            newobj.transform.parent = transform;

            spheresList.Add(newobj);
        }
    }

    public GameObject CreateEdgeSphere(Vector3 position)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity);
        return obj;
    }

    public void TurnOnRagdoll()
    {
        RigidbodyComponent.useGravity = false;
        RigidbodyComponent.linearVelocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
        skinedMeshAnimator.enabled = false;
        skinedMeshAnimator.avatar = null;

        foreach (Collider c in ragdollParts)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = false;
                c.attachedRigidbody.linearVelocity = Vector3.zero;
            }
        }
    }

    // -----

    private void Awake()
    {
        bool switchBack = false;

        if (IsFacingForward())
        {
            switchBack = true;
        }

        FaceForward(true);

        SetColliderSphere();

        if (switchBack)
        {
            FaceForward(false);
        }

    }

    private void Start()
    {
        SceneLinkedSMB<CharacterControl>.Initialise(skinedMeshAnimator, this);
    }

    private void FixedUpdate()
    {
        if (RigidbodyComponent.linearVelocity.y < 0f) // Player Going Down
        {
            RigidbodyComponent.linearVelocity += (-Vector3.up * gravityMultiplier);
        }

        if (RigidbodyComponent.linearVelocity.y > 0f && jump) // Player Going Up
        {
            RigidbodyComponent.linearVelocity += (-Vector3.up * pullMultiplier);
        }
    }

    // -----

    private void SetColliderSphere()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        float top = box.bounds.center.y + box.bounds.extents.y;
        float bottom = box.bounds.center.y - box.bounds.extents.y;
        float front = box.bounds.center.z + box.bounds.extents.z;
        float back = box.bounds.center.z - box.bounds.extents.z;

        GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
        GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

        bottomFront.transform.parent = transform;
        bottomBack.transform.parent = transform;
        topFront.transform.parent = transform;

        bottomFront.transform.localPosition = transform.InverseTransformPoint(bottomFront.transform.position);
        bottomBack.transform.localPosition = transform.InverseTransformPoint(bottomBack.transform.position);
        topFront.transform.localPosition = transform.InverseTransformPoint(topFront.transform.position);

        bottomSpheres.Add(bottomFront);
        bottomSpheres.Add(bottomBack);

        frontSpheres.Add(bottomFront);
        frontSpheres.Add(topFront);

        float horizontalSection = (bottomFront.transform.position - bottomBack.transform.position).magnitude * 0.2f; // magnitude / 5f
        CreateMiddleSpheres(bottomBack, transform.forward, horizontalSection, 4, bottomSpheres);

        float varticalSection = (topFront.transform.position - bottomFront.transform.position).magnitude * 0.1f; // magnitude / 10f
        CreateMiddleSpheres(bottomFront, transform.up, varticalSection, 9, frontSpheres);
    }
}
