using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidIdle")]
    public class HumanoidIdle : StateData<HumanoidController>
    {
        public override void OnStart(Animator animator)
        {

        }

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animator.IsInTransition(layerIndex))
            {
                if (monoBehaviour.attack && !animator.GetBool(CharacterControl.TransitionParameter.Attack.ToString()))
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), true);
                }

                if (monoBehaviour.jump && !animator.GetBool(CharacterControl.TransitionParameter.Jump.ToString()))
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
                }

                Vector2 move = monoBehaviour.move;

                if (move.magnitude > 0)
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
                }
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}