using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/Landing")]
    public class Landing : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TEMP
            if (animator.IsInTransition(layerIndex))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex1)
        {
        }
    }
}
