using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EncounterController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;
    public Transform WeaponMountPosition;

    //[Border]
    private PlayerInputController playerInputCtrl;

    private bool m_isRotating;
    private bool m_isMoving;

    private NavMeshAgent m_NavMeshAgent;

    // -----

    public void Equip(PlayerController playerController)
    {
        playerController.transform.SetParent(WeaponMountPosition.transform);
        playerController.transform.localPosition = Vector3.zero; ;
        playerController.transform.localRotation = Quaternion.identity;
        m_NavMeshAgent.enabled = false;
    }

    public void Unequip()
    {
        m_NavMeshAgent.enabled = true;
        transform.SetParent(null);
        this.enabled = false;
    }

    // -----

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        playerInputCtrl = PlayerInputController.Instance;
    }

    private void Start()
    {
        this.enabled = false ;
    }

    void Update()
    {
        float xInput = playerInputCtrl.MoveInput.x;
        float yInput = playerInputCtrl.MoveInput.y;

        Move(xInput);
    }

    private void OnEnable()
    {
        playerInputCtrl.Crounch.AddListener(Unequip);
    }

    private void OnDisable() 
    {
        playerInputCtrl.Crounch.RemoveListener(Unequip);
    }

    // -----

    private void Move(float direction)
    {
        // 이동 방향
        var move = new Vector3(direction, 0f, 0f);

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
