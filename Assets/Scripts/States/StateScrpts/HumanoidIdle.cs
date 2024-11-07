using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidIdle")]
    public class HumanoidIdle : StateData<HumanoidController>
    {
        private InputController _controller;

        public override void OnStart(Animator animator)
        {
            _controller = InputController.Instance;
        }

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(_controller.JumpInput)
            {
                animator.SetBool(monoBehaviour.hashJump, true);
            }

            Vector2 move = _controller.MoveInput;

            if (move.magnitude > 0)
            {
                animator.SetBool(monoBehaviour.hashMove, true);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}