using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidForceTransition")]
    public class HumanoidForceTransition : StateData<HumanoidController>
    {
        [Range(0.01f, 1f)]
        public float transitionTiming;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= transitionTiming)
            {
                animator.SetBool(monoBehaviour.hashForceTransition, true);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(monoBehaviour.hashForceTransition, false);
        }
    }
}