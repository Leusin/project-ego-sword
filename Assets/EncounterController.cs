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
        // �̵� ����
        var move = new Vector3(direction, 0f, 0f);

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
