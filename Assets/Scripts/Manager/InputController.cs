using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace ProjectEgoSword
{
    public class InputController : MonoBehaviour
    {
        [Header("Karl")]

        [SerializeField] private bool _attract;
        [SerializeField] private Vector2 _look;

        [Header("Humanoid")]

        [SerializeField] private Vector2 _move;

        public bool AttractInput { get { return _attract; } }
        public Vector2 LookInput { get { return _look; } }
        public Vector2 MoveInput { get { return _move; } }

        // -----

        /** InputAction.CallbackContext ���� ����
        * 
        * context.action.phase ���� InputActionPhase��� �����Ͱ� ������, 5���� Ÿ���� �ֽ��ϴ�.
        * 
        *   Started: ���� ���� �� ȣ��
        *   Performed: ���� Ȯ�� (������ ����) �� ȣ��
        *   Canceled: ���� ���� �� ȣ��
        *   Disabled: �׼��� Ȱ��ȭ���� ����
        *   Waiting: �׼��� Ȱ��ȭ�Ǿ��ְ� �Է��� ��ٸ��� ����
        *   
        * �Ϲ����� ��ư �Է� ��Ȳ (Default)������ Started�� Performed�� ���� Ÿ�ֿ̹� ����ǰ�(���� ������ Started�� ����)
        * �׸��� Canceled�� ��ư�� ���� �� ����˴ϴ�.
        * 
        * ��ư�� ���� �Է� ó���� �� �� Started, Performed, Canceled ��Ȳ�� ���� �ٸ� ó���� �Ϸ��� ���
        * ���� ����ġ���� �����Ͽ� �ڵ带 �ۼ��ؾ� �մϴ�.
        * 
        * switch (context.phase)
        * {
        *     case InputActionPhase.Performed:
        *         break;
        * 
        *     case InputActionPhase.Started:
        *         _attract = true;
        *         break;
        * 
        *     case InputActionPhase.Canceled:
        *         _attract = false;
        *         break;
        * }
        *
        */

        public void Attract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _attract = true;
            }
            else
            {
                _attract = false;
            }
        }

        public void Look(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        public void MainAttack(InputAction.CallbackContext context)
        {
        }

        public void AltAttack(InputAction.CallbackContext context)
        {
        }

        public void Crounch(InputAction.CallbackContext context)
        {
        }

        public void Dash(InputAction.CallbackContext context)
        {
        }

        public void Interact(InputAction.CallbackContext context)
        {
        }

        public void Jump(InputAction.CallbackContext context)
        {
        }

        public void Move(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        public void Sprint(InputAction.CallbackContext context)
        {
        }

        // -----

        public static InputController s_instance;
        public static InputController Instance { get { return s_instance; } }

        void Awake()
        {
            if (s_instance == null)
                s_instance = this;
            else if (s_instance != this)
                throw new UnityException($"PlayerInputCtroller script �� �ϳ� ���� ���Ƽ��� �ȵ˴ϴ�. The instances are " + s_instance.name + " and " + name + ".");
        }
    }
}
