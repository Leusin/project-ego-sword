using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidIdle")]
    public class HumanoidIdle : StateData<HumanoidController>
    {
        public override void OnStart(Animator animator)
        {

        }

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(monoBehaviour.hashJump, false);
            animator.SetBool(monoBehaviour.hashAttack, false);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animator.IsInTransition(layerIndex))
            {
                if (monoBehaviour.attack && !animator.GetBool(monoBehaviour.hashAttack))
                {
                    animator.SetBool(monoBehaviour.hashAttack, true);
                }

                if (monoBehaviour.jump && !animator.GetBool(monoBehaviour.hashJump))
                {
                    animator.SetBool(monoBehaviour.hashJump, true);
                }

                Vector2 move = monoBehaviour.move;

                if (move.magnitude > 0)
                {
                    animator.SetBool(monoBehaviour.hashMove, true);
                }
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}