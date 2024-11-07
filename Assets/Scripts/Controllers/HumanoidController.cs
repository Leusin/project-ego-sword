using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{

    public class HumanoidController : MonoBehaviour
    {
        public Transform WeaponMountPosition;

        // -----

        [Header("Setup")]
        public Animator animator;
        public GameObject ColliderEdgePrefab;

        [SerializeField]
        public List<GameObject> BottomSpheres = new List<GameObject>();

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

            bottomFront.transform.parent = transform;
            bottomBack.transform.parent = transform;

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            float section = (bottomFront.transform.position - bottomBack.transform.position).magnitude * 0.2f; // magnitude / 5f

            for (int i = 0; i < 4; i++)
            {
                Vector3 position = bottomBack.transform.position + (Vector3.forward * section * (i + 1));
                GameObject newobj = CreateEdgeSphere(position);

                newobj.transform.parent = transform;
                BottomSpheres.Add(newobj);
            }
        }

        private void Start()
        {
            SceneLinkedSMB<HumanoidController>.Initialise(animator, this);
        }

        // -----

        public GameObject CreateEdgeSphere(Vector3 position)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity);
            return obj;
        }
    }
}