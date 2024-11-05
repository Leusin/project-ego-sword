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

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotateSpeed = 1f;

        [Header("Setup")]
        public Animator animator;

        private NavMeshAgent _navMeshAgent;

        private readonly int _hashMoveSpeed = Animator.StringToHash("move_speed");

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

        private void Update()
        {
            Vector2 move = PlayerInputController.Instance.MoveInput;

            Vector3 direction = Vector3.forward;
            Quaternion rotate = Quaternion.identity;
            var animSpeed = 0;

            if (move.x > 0)
            {
                rotate = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (move.x < 0)
            {
                rotate = Quaternion.Euler(0f, -90f, 0f);
            }

            if (move.magnitude > 0)
            {
                transform.Translate(direction * Time.deltaTime * moveSpeed);
                transform.rotation = rotate;
                animSpeed = 2;
            }

            animator.SetFloat(_hashMoveSpeed, animSpeed);

            Debug.Log($"휴머노이드는 업데이트중: {move}, {animSpeed}");
        }
    }
}