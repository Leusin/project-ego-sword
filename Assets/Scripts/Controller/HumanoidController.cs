using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ProjectEgoSword
{

    public class HumanoidController : MonoBehaviour
    {
        public Transform WeaponMountPosition;

        // -----

        public float moveSpeed = 10f;
        [SerializeField] private float rotateSpeed = 1f;

        [Header("Setup")]
        public Animator animator;

        private NavMeshAgent _navMeshAgent;

        // FSM 전이 매개 변수(Transition Parameter)
        [HideInInspector] public readonly int hashMove = Animator.StringToHash("Move");
        [HideInInspector] public readonly int hashJump = Animator.StringToHash("Jump");
        [HideInInspector] public readonly int hashForceTransition = Animator.StringToHash("ForceTransition");

        // -----

        public void Equip(PlayerController playerController)
        {
            playerController.transform.SetParent(WeaponMountPosition.transform);
            playerController.transform.localPosition = Vector3.zero;
            playerController.transform.localRotation = Quaternion.identity;
            _navMeshAgent.enabled = false;
        }

        public void Unequip()
        {
            _navMeshAgent.enabled = true;
            transform.SetParent(null);
            this.enabled = false;
        }

        // -----

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            SceneLinkedSMB<HumanoidController>.Initialise(animator, this);
        }
    }
}