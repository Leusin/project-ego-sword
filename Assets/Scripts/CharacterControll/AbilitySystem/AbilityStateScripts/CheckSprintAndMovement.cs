using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckSprintAndMovement")]
    public class CheckSprintAndMovement : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if ((monobehaviour.moveLeft || monobehaviour.moveLeft) && monobehaviour.rash)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
                animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), true);
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
                animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), false);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}