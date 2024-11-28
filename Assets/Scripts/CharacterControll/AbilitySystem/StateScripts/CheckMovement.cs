using System.Threading;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckMove")]
    public class CheckMovement : StateData<CharacterControl>
    {
        float timer;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer = 0f;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.moveLeft || monobehaviour.moveRight)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
                timer = 0f;
            }
            else if(timer > 1f)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
            }
            else
            {
                timer++;
            }

        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}