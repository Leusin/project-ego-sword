using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/ForceTransition")]
    public class ForceTransition : StateData<CharacterControl>
    {
        [Range(0.01f, 1f)]
        public float transitionTiming;

        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= transitionTiming)
            {
                animator.SetBool(CharacterControl.TransitionParameter.ForceTransition.ToString(), true);
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.ForceTransition.ToString(), false);
        }
    }
}