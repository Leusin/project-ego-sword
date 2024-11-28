using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckSprint")]
    public class CheckSprint : StateData<CharacterControl>
    {
        public bool mustRequireMovemnt;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.rash)
            {
                if (mustRequireMovemnt)
                {
                    if (monobehaviour.moveLeft || monobehaviour.moveRight)
                    {
                        animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), true);
                    }
                }
                else
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), false);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}