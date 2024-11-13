using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidLanding")]
    public class HumanoidLanding : StateData<HumanoidController>
    {
        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TEMP
            if (animator.IsInTransition(layerIndex))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex1)
        {
        }
    }
}
