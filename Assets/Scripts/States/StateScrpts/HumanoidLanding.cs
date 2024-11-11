using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidLanding")]
    public class HumanoidLanding : StateData<HumanoidController>
    {
        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(monoBehaviour.hashJump, false);
            animator.SetBool(monoBehaviour.hashMove, false);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TEMP
            if (animator.IsInTransition(layerIndex))
            {
                animator.SetBool(monoBehaviour.hashJump, false);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex1)
        {
        }
    }
}
