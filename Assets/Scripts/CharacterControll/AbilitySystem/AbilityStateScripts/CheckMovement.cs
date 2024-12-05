using System.Threading;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckMove")]
    public class CheckMovement : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.moveLeft || monobehaviour.moveRight)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}