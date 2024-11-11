using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace ProjectEgoSword
{
    public class VirtualInputController : Singleton<VirtualInputController>
    {
        [Header("Karl")]

        [SerializeField] private bool _attract;
        [SerializeField] private Vector2 _look;

        [Header("Humanoid")]

        [SerializeField] private Vector2 _move;
        [SerializeField] private bool _jump;
        [SerializeField] private bool _attack;

        public bool AttractInput { get { return _attract; } }
        public Vector2 LookInput { get { return _look; } }
        public Vector2 MoveInput { get { return _move; } }
        public bool JumpInput { get { return _jump; } }
        public bool AttackInput { get { return _attack; } }

        // -----

        /** InputAction.CallbackContext 관련 참고
        * 
        * context.action.phase 에는 InputActionPhase라는 데이터가 있으며, 5가지 타입이 있습니다.
        * 
        *   Started: 실행 시작 시 호출
        *   Performed: 실행 확정 (완전히 실행) 시 호출
        *   Canceled: 실행 종료 시 호출
        *   Disabled: 액션이 활성화되지 않음
        *   Waiting: 액션이 활성화되어있고 입력을 기다리는 상태
        *   
        * 일반적인 버튼 입력 상황 (Default)에서는 Started와 Performed가 같은 타이밍에 실행되고(실행 순서는 Started가 먼저)
        * 그리고 Canceled는 버튼을 뗐을 때 실행됩니다.
        * 
        * 버튼과 같은 입력 처리를 할 떄 Started, Performed, Canceled 상황에 따라 다른 처리를 하려는 경우
        * 다음 스위치문을 참고하여 코드를 작성해야 합니다.
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
            if (context.phase == InputActionPhase.Performed)
            {
                _attack = true;
            }
            else
            {
                _attack = false;
            }
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
            if (context.phase == InputActionPhase.Performed)
            {
                _jump = true;
            }
            else
            {
                _jump = false;
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        public void Sprint(InputAction.CallbackContext context)
        {
        }

        // -----

        //public static VirtualInputController s_instance;
        //public static VirtualInputController Instance { get { return s_instance; } }

        //void Awake()
        //{
        //    if (s_instance == null)
        //        s_instance = this;
        //    else if (s_instance != this)
        //        throw new UnityException($"PlayerInputCtroller script 는 하나 보다 많아서는 안됩니다. The instances are " + s_instance.name + " and " + name + ".");
        //}
    }
}
