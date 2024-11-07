using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidIdle")]
    public class HumanoidIdle : StateData<HumanoidController>
    {
        public override void OnStart(Animator animator)
        {
            
        }

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(monoBehaviour.hashJump, false);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            InputController controller = InputController.Instance;

            if (controller.JumpInput)
            {
                animator.SetBool(monoBehaviour.hashJump, true);
            }

            Vector2 move = controller.MoveInput;

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