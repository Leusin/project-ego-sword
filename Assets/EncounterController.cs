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
        // �̵� ����
        Vector3 move = new Vector3(m_direction, 0f, 0f);

        m_isMoving = move.sqrMagnitude > 0f;

        // �ٶ󺸱�(ȸ���ϱ�)
        if (m_isMoving)
        {
            StartCoroutine(RotateTowards(move));
        }

        // �̵��ϱ�
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
            // ���� ����� ��ǥ ���� ������ ���� ���� ���
            float angleDifference = Vector3.Angle(transform.forward, targetDirection);

            // ���� ���̰� 5�� �̳��� �پ��� ȸ���� ���߰� ����
            if (angleDifference <= 5f)
            {
                transform.forward = targetDirection; // ��ǥ �������� ����
                m_isRotating = false;
                yield break; // �ڷ�ƾ ����
            }

            m_isRotating = true;

            // ȸ�� �ӵ��� ���� ���� ������ ȸ��
            float scaledRotateSpeed = rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, scaledRotateSpeed);

            // ���� �����ӱ��� ���
            yield return null;
        }
    }
}
