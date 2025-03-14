using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/Idle")]
    public class Idle : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);

            monoBehaviour.animationProgress.disallowEarylTurn = false;
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animator.IsInTransition(layerIndex))
            {
                if (monoBehaviour.attack && !animator.GetBool(CharacterControl.TransitionParameter.Attack.ToString()))
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), true);
                }
                
                if (monoBehaviour.jump && !animator.GetBool(CharacterControl.TransitionParameter.Jump.ToString()))
                {
                    if(!monoBehaviour.animationProgress.jumped)
                    {
                        animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
                    }
                }
                else
                {
                    monoBehaviour.animationProgress.jumped = false;
                }

                if (monoBehaviour.moveLeft || monoBehaviour.moveRight)
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
                }
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);
        }
    }
}