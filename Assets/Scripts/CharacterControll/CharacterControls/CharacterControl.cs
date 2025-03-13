using Roundbeargames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
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
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        [Header("Gravity")]
        public float gravityMultiplier;
        public float pullMultiplier;
        public ContactPoint[] contactPoints;

        [Header("Setup")]
        public bool faceForword;
        public Animator skinnedMeshAnimator;
        public List<Collider> ragdollParts = new List<Collider>();
        public GameObject leftHandAttack;
        public GameObject rightHandAttack;
        public GameObject leftfootAttack;
        public GameObject rightfootAttack;

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

            // Meaning if Ragdoll is Turn on
            if(!skinnedMeshAnimator.enabled)
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

                    c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                    c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                    CharacterJoint joint = c.GetComponent<CharacterJoint>();

                    if (joint != null)
                    {
                        joint.enableProjection = true;
                    }

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

        public void TurnOnRagdoll()
        {
            // Change Layers
            Transform[] arr = GetComponentsInChildren<Transform>();
            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(ProjectES_Layers.DeadBody.ToString());
            }

            // Save BodyPart Position
            foreach (Collider c in ragdollParts)
            {
                TriggerDetector triggerDetec = c.GetComponent<TriggerDetector>();
                triggerDetec.lastPostion = c.gameObject.transform.localPosition;
                triggerDetec.lastRotation = c.gameObject.transform.localRotation;
            }

            // Turn off animator/avatar/etc
            RigidbodyComponent.useGravity = false;
            RigidbodyComponent.linearVelocity = Vector3.zero;
            GetComponent<Collider>().enabled = false;
            skinnedMeshAnimator.enabled = false;
            skinnedMeshAnimator.avatar = null;

            // Turn on ragdoll
            foreach (Collider c in ragdollParts)
            {
                if (c.gameObject != gameObject)
                {
                    c.isTrigger = false;

                    TriggerDetector triggerDetec = c.GetComponent<TriggerDetector>();
                    c.transform.localPosition = triggerDetec.lastPostion;
                    c.transform.localRotation = triggerDetec.lastRotation;

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

        public void UpdateBoxColliderSiez()
        {
            if (!animationProgress.updatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.targetSize) > 0.01f)
            {
                boxCollider.size = Vector3.Lerp(boxCollider.size,
                    animationProgress.targetSize, Time.deltaTime * animationProgress.sizeSpeed);

                animationProgress.updatingSpheres = true;
            }
        }

        public void UpdateBoxColliderCenter()
        {
            if (!animationProgress.updatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.targetCenter) > 0.01f)
            {
                boxCollider.center = Vector3.Lerp(boxCollider.center,
                    animationProgress.targetCenter, Time.deltaTime * animationProgress.centerSpeed);

                animationProgress.updatingSpheres = true;
            }
        }

        public void RepositionFrontSpheres()
        {
            float top = boxCollider.bounds.center.y + boxCollider.bounds.size.y / 2f;
            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2f;
            float front = boxCollider.bounds.center.z + boxCollider.bounds.size.z / 2f;

            frontSpheres[0].transform.localPosition = new Vector3(0f, bottom + 0.05f, front) - transform.position;
            frontSpheres[1].transform.localPosition = new Vector3(0f, top, front) - transform.position;

            float interval = (top - bottom + 0.05f) / 9f;

            for (int i = 2; i < frontSpheres.Count; i++)
            {
                frontSpheres[i].transform.localPosition = new Vector3(0f, bottom + (interval * (i - 1)), front) - transform.position;
            }
        }

        public void RepositionBottomSpheres()
        {
            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2f;
            float front = boxCollider.bounds.center.z + boxCollider.bounds.size.z / 2f;
            float back = boxCollider.bounds.center.z - boxCollider.bounds.size.z / 2f;

            bottomSpheres[0].transform.localPosition = new Vector3(0f, bottom, back) - transform.position;
            bottomSpheres[1].transform.localPosition = new Vector3(0f, bottom, front) - transform.position;

            float interval = (front - back) / 4f;

            for (int i = 2; i < bottomSpheres.Count; i++)
            {
                bottomSpheres[i].transform.localPosition = new Vector3(0f, bottom, back + (interval * (i - 1))) - transform.position;
            }
        }

        // -----

        private void Awake()
        {
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDetector>();
            aiController = GetComponentInChildren<AIController>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            SetColliderSphere();
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
            if (!animationProgress.cancelPull)
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
            
            animationProgress.updatingSpheres = false;
            UpdateBoxColliderSiez();
            UpdateBoxColliderCenter();

            if (animationProgress.updatingSpheres)
            {
                RepositionFrontSpheres();
                RepositionBottomSpheres();
            }

            if (animationProgress.ragdollTriggerd)
            {
                TurnOnRagdoll();
                animationProgress.ragdollTriggerd = false;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            contactPoints = collision.contacts;
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
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;

                bottomSpheres.Add(obj);
                obj.transform.parent = transform;
            }

            RepositionBottomSpheres();

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;

                frontSpheres.Add(obj);
                obj.transform.parent = transform;
            }

            RepositionFrontSpheres();
        }
    }
}
