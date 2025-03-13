using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }

    // Karl
    public Action<InputAction.CallbackContext> Attaract;
    // Man
    public Action<InputAction.CallbackContext> AttackMain, AttackAlt, Crounch, Dash, Interact, Jump, Look, Move, Sprint;

    // -----

    public void OnAttaract(InputAction.CallbackContext context)
    {
        Attaract?.Invoke(context);
    }

    public void OnMainAttack(InputAction.CallbackContext context)
    {
        AttackMain?.Invoke(context);
    }

    public void OnAltAttack(InputAction.CallbackContext context)
    {
        AttackAlt?.Invoke(context);
    }
    
    public void OnCrounch(InputAction.CallbackContext context)
    {
        Crounch?.Invoke(context);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Dash?.Invoke(context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Interact?.Invoke(context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump?.Invoke(context);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look?.Invoke(context);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move?.Invoke(context);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Sprint?.Invoke(context);
    }

    // -----
    private void Awake()
    {
        // �̱��� ���� �ʱ�ȭ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ı�
            Debug.Log("There cannot be more than one PlayerInput script.  The instances are " + Instance.name + " and " + name + ".");

            return;
        }

        Instance = this;
    }
}
