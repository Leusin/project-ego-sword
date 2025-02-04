using ProjectEgoSword;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum TransitionParameter
    {
        Dash,
        Move,
        Jump,
        Turn,
        Attack,
        Sprint,
        Grounded,
        ClickAnimation,
        TransitionIndex,
        ForceTransition,
    }

    public PlayableCharacterType characterType;

    [Header("Input")]
    public Vector2 move;
    public bool moveLeft;
    public bool moveRight;
    public bool moveUp;
    public bool moveDown;
    public bool jump;
    public bool attack;
    public bool rash;

    [Header("SubComponents")]
    public Material material;
    public LedgeChecker ledgeChecker;
    public AnimationProgress animationProgress;
    public AIProgress aiProgress;
    public DamageDetector damageDetector;
    //public GameObject colliderEdgePrefab;
    public List<GameObject> bottomSpheres = new List<GameObject>();
    public List<GameObject> frontSpheres = new List<GameObject>();

    [Header("Gravity")]
    public float gravityMultiplier;
    public float pullMultiplier;

    [Header("Setup")]
    public bool faceForword;
    public Animator skinnedMeshAnimator;
    public List<Collider> ragdollParts = new List<Collider>();
    public GameObject leftHandAttack;
    public GameObject rightHandAttack;

    private List<TriggerDetector> _triggerDetectors = new List<TriggerDetector>();
    private Dictionary<string, GameObject> _childObjects = new Dictionary<string, GameObject>();

    private Rigidbody _cachedrigidbody;
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

    // -----

    public void MoveForward(float speed, float speedGraph)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * speedGraph);
    }

    public void FaceForward(bool forward)
    {
        if (!faceForword)
        {
            return;
        }

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
            return true;
        }
        else
        {
            return false;
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
        if (material == null)
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
        GameObject obj = Instantiate(Resources.Load("ColliderEdge", 
            typeof(GameObject)), 
            position,
            Quaternion.identity ) as GameObject;

        return obj;
    }

    public void TurnOnRagdoll()
    {
        RigidbodyComponent.useGravity = false;
        RigidbodyComponent.linearVelocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
        skinnedMeshAnimator.enabled = false;
        skinnedMeshAnimator.avatar = null;

        foreach (Collider c in ragdollParts)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = false;
                c.attachedRigidbody.linearVelocity = Vector3.zero;
            }
        }
    }

    public GameObject GetChildObject(string name)
    {
        if (_childObjects.ContainsKey(name))
        {
            return _childObjects[name];
        }

        Transform[] arr = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform t in arr)
        {
            if (t.gameObject.name.Equals(name))
            {
                _childObjects.Add(name, t.gameObject);
                return t.gameObject;
            }
        }

        return null;
    }

    public Collider GetBodyPart(string name)
    {
        foreach (Collider c in ragdollParts)
        {
            return c;
        }

        return null;
    }

    // -----

    private void Awake()
    {
        bool switchBack = false;

        if (!IsFacingForward())
        {
            switchBack = true;
        }

        FaceForward(true);

        if (switchBack)
        {
            FaceForward(false);
        }

        SetColliderSphere();

        ledgeChecker = GetComponentInChildren<LedgeChecker>();
        animationProgress = GetComponent<AnimationProgress>();
        aiProgress = GetComponentInChildren<AIProgress>();
        damageDetector = GetComponentInChildren<DamageDetector>();

        RegisterCharacter();
    }

    private void Start()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();

        for (int i = 0; i < animators.Length; i++)
        {
            SceneLinkedSMB<CharacterControl>.Initialise(animators[i], this);
        }

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

    private void RegisterCharacter()
    {
        if (!CharacterManager.Instance.characters.Contains(this))
        {
            CharacterManager.Instance.characters.Add(this);
        }
    }

    private void SetColliderSphere()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        float top = box.bounds.center.y + box.bounds.extents.y;
        float bottom = box.bounds.center.y - box.bounds.extents.y;
        float front = box.bounds.center.z + box.bounds.extents.z;
        float back = box.bounds.center.z - box.bounds.extents.z;

        GameObject bottomFrontHor = CreateEdgeSphere(new Vector3(0f, bottom, front));
        GameObject bottomFrontVar = CreateEdgeSphere(new Vector3(0f, bottom + 0.1f, front));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
        GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

        bottomFrontHor.transform.parent = transform;
        bottomFrontVar.transform.parent = transform;
        bottomBack.transform.parent = transform;
        topFront.transform.parent = transform;

        bottomFrontHor.transform.localPosition = transform.InverseTransformPoint(bottomFrontHor.transform.position);
        bottomFrontVar.transform.localPosition = transform.InverseTransformPoint(bottomFrontVar.transform.position);
        bottomBack.transform.localPosition = transform.InverseTransformPoint(bottomBack.transform.position);
        topFront.transform.localPosition = transform.InverseTransformPoint(topFront.transform.position);

        bottomSpheres.Add(bottomFrontHor);
        bottomSpheres.Add(bottomBack);

        // 초기화 순서에 따라
        // 0 번 인덱스 맨 밑, 1 번 인덱스 맨 위
        // 그리고 그 후엔 바닥 -> 천장 순서로 그사이에 배치되어있음

        frontSpheres.Add(bottomFrontVar);
        frontSpheres.Add(topFront);

        float horizontalSection = (bottomFrontHor.transform.position - bottomBack.transform.position).magnitude * 0.2f; // magnitude / 5f
        CreateMiddleSpheres(bottomBack, transform.forward, horizontalSection, 4, bottomSpheres);

        float varticalSection = (topFront.transform.position - bottomFrontVar.transform.position).magnitude * 0.1f; // magnitude / 10f
        CreateMiddleSpheres(bottomFrontVar, transform.up, varticalSection, 9, frontSpheres);
    }
}
