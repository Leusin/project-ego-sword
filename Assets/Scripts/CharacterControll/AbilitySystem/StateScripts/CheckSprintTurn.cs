using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckSprintTurn")]
    public class CheckSprintTurn : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!animator.IsInTransition(layerIndex))
            {
                if (monobehaviour.IsFacingForward())
                {
                    if (monobehaviour.moveLeft)
                    {
                        animator.SetBool(CharacterControl.TransitionParameter.Turn.ToString(), true);
                    }
                }
                else
                {
                    if (monobehaviour.moveRight)
                    {
                        animator.SetBool(CharacterControl.TransitionParameter.Turn.ToString(), true);
                    }
                }
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Turn.ToString(), false);
        }
    }
}