using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class EncounterController : MonoBehaviour
{
    public Transform WeaponMountPosition;

    // -----

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;

    private bool m_isRotating;
    private bool m_isMoving;
    private float m_direction;

    private NavMeshAgent m_navMeshAgent;

    private PlayerInputController m_playerInputCtrl;

    // -----

    public void Equip(PlayerController playerController)
    {
        playerController.transform.SetParent(WeaponMountPosition.transform);
        playerController.transform.localPosition = Vector3.zero; ;
        playerController.transform.localRotation = Quaternion.identity;
        m_navMeshAgent.enabled = false;
    }

    public void Unequip()
    {
        m_navMeshAgent.enabled = true;
        transform.SetParent(null);
        this.enabled = false;
    }

    // -----

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        this.enabled = false;
    }


    private void OnEnable()
    {
        m_playerInputCtrl = PlayerInputController.Instance;

        if (m_playerInputCtrl)
        {
            m_playerInputCtrl.Move += Move;
        }
    }

    private void OnDisable()
    {
        if (m_playerInputCtrl)
        {
            m_playerInputCtrl.Move -= Move;
        }
    }

    private void Update()
    {
        // 이동 방향
        Vector3 move = new Vector3(m_direction, 0f, 0f);

        m_isMoving = move.sqrMagnitude > 0f;

        // 바라보기(회전하기)
        if (m_isMoving)
        {
            StartCoroutine(RotateTowards(move));
        }

        // 이동하기
        if (m_isRotating == false)
        {
            var scaledMoveSpeed = moveSpeed * Time.deltaTime;
            transform.position += move * scaledMoveSpeed;
        }
    }

    // -----

    private void Move(InputAction.CallbackContext context)
    {
        m_direction = context.ReadValue<Vector2>().x;
    }

    private IEnumerator RotateTowards(Vector3 targetDirection)
    {
        while (true)
        {
            // 현재 방향과 목표 방향 사이의 각도 차이 계산
            float angleDifference = Vector3.Angle(transform.forward, targetDirection);

            // 각도 차이가 5도 이내로 줄어들면 회전을 멈추고 종료
            if (angleDifference <= 5f)
            {
                transform.forward = targetDirection; // 목표 방향으로 설정
                m_isRotating = false;
                yield break; // 코루틴 종료
            }

            m_isRotating = true;

            // 회전 속도에 따라 일정 각도로 회전
            float scaledRotateSpeed = rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, scaledRotateSpeed);

            // 다음 프레임까지 대기
            yield return null;
        }
    }
}
