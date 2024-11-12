using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{

    public class HumanoidController : CharacterContrl
    {
        [Header("Setup")]
        public Animator skinedMeshAnimator;
        public GameObject ColliderEdgePrefab;
        public Transform WeaponMountPosition;

        [Header("Input")]
        public Vector2 move;
        public bool jump;
        public bool attack;

        /*[HideInInspector]*/ public List<GameObject> bottomSpheres = new List<GameObject>();
        /*[HideInInspector]*/ public List<GameObject> frontSpheres = new List<GameObject>();
        /*[HideInInspector]*/ public List<Collider> ragdollParts = new List<Collider>();
        /*[HideInInspector]*/ public List<Collider> collidingParts = new List<Collider>();

        [HideInInspector] public float gravityMultiplier;
        [HideInInspector] public float pullMultiplier;

        //
        // 캐시된 컴포넌트
        //

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

        public NavMeshAgent NavMeshAgentComponent
        {
            get
            {
                if (_cachednavMeshAgent == null)
                {
                    _cachednavMeshAgent = GetComponent<NavMeshAgent>();
                }
                return _cachednavMeshAgent;
            }
        }

        private Rigidbody _cachedrigidbody;
        private NavMeshAgent _cachednavMeshAgent;

        //
        // FSM 전이 매개 변수(Transition Parameter)
        //

        public readonly int hashMove = Animator.StringToHash("Move");
        public readonly int hashJump = Animator.StringToHash("Jump");
        public readonly int hashAttack = Animator.StringToHash("Attack");
        public readonly int hashForceTransition = Animator.StringToHash("ForceTransition");
        public readonly int hashGrounded = Animator.StringToHash("Grounded");

        // -----

        public void Equip(PlayerController playerController)
        {
            playerController.transform.SetParent(WeaponMountPosition.transform);
            playerController.transform.localPosition = Vector3.zero;
            playerController.transform.localRotation = Quaternion.identity;
            _cachednavMeshAgent.enabled = false;
        }

        public void Unequip()
        {
            _cachednavMeshAgent.enabled = true;
            transform.SetParent(null);
            this.enabled = false;
        }

        // -----

        private void Awake()
        {
            bool switchBack = false;

            if(IsFacingForward())
            {
                switchBack = true;
            }

            FaceForward(true);

            SetRagdollParts();
            SetColliderSphere();

            if(switchBack)
            {
                FaceForward(false);
            }
        }

        private void Start()
        {
            SceneLinkedSMB<HumanoidController>.Initialise(skinedMeshAnimator, this);
        }

        //private IEnumerator RagdollTest()
        //{
        //    yield return new WaitForSeconds(5f);
        //    RigidbodyComponent.AddForce(200f * Vector3.up);
        //    yield return new WaitForSeconds(0.5f);
        //    TurnOnRagdoll();
        //}

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

        private void OnTriggerEnter(Collider other)
        {
            if(ragdollParts.Contains(other))
            {
                return;
            }

            var controller = other.transform.root.GetComponent<CharacterContrl>();

            if (controller == null)
            {
                return;
            }

            if(other.gameObject == controller.gameObject)
            {
                return;
            }

            if(!collidingParts.Contains(other))
            {
                collidingParts.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(collidingParts.Contains(other))
            {
                collidingParts.Remove(other);
            }
        }

        // -----

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

        public void MoveForward(float speed, float speedGraph)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * speedGraph);
        }

        public void FaceForward(bool forward)
        {
            if(forward)
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

        private void SetRagdollParts()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if(c.gameObject != gameObject)
                {
                    c.isTrigger = true;
                    ragdollParts.Add(c);
                }
            }
        }

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
}