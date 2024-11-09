using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static System.Collections.Specialized.BitVector32;

namespace ProjectEgoSword
{

    public class HumanoidController : MonoBehaviour
    {
        public Transform WeaponMountPosition;

        // -----

        [Header("Setup")]
        public Animator animator;
        public GameObject ColliderEdgePrefab;

        [Header("Input")]
        public bool jump;

        [HideInInspector] public List<GameObject> BottomSpheres = new List<GameObject>();
        [HideInInspector] public List<GameObject> FrontSpheres = new List<GameObject>();

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

        [HideInInspector] public readonly int hashMove = Animator.StringToHash("Move");
        [HideInInspector] public readonly int hashJump = Animator.StringToHash("Jump");
        [HideInInspector] public readonly int hashForceTransition = Animator.StringToHash("ForceTransition");
        [HideInInspector] public readonly int hashGrounded = Animator.StringToHash("Grounded");

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

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(bottomFront);
            FrontSpheres.Add(topFront);

            float horizontalSection = (bottomFront.transform.position - bottomBack.transform.position).magnitude * 0.2f; // magnitude / 5f
            CreateMiddleSpheres(bottomBack, transform.forward, horizontalSection, 4, BottomSpheres);

            float varticalSection = (topFront.transform.position - bottomFront.transform.position).magnitude * 0.1f; // magnitude / 10f
            CreateMiddleSpheres(bottomFront, transform.up, varticalSection, 9, FrontSpheres);
        }

        private void Start()
        {
            SceneLinkedSMB<HumanoidController>.Initialise(animator, this);
        }

        private void FixedUpdate()
        {
            if (RigidbodyComponent.linearVelocity.y < 0f) // Player Going Down
            {
                RigidbodyComponent.linearVelocity += (-Vector3.up * gravityMultiplier);
            }

            if (RigidbodyComponent.linearVelocity.y > 0f && InputController.Instance.JumpInput) // Player Goding Up
            {
                RigidbodyComponent.linearVelocity += (-Vector3.up * pullMultiplier);
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
    }
}