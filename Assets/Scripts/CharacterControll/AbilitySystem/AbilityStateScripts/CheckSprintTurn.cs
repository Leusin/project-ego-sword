using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckSprintTurn")]
    public class CheckSprintTurn : StateData<CharacterControl>
    {
        public float activationDelay;
        private float _enterTime;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _enterTime = Time.time;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if ((Time.time - _enterTime) < activationDelay)
            {
                return;
            }

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
            if (!animator.IsInTransition(layerIndex))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Turn.ToString(), false);
            }
        }
    }
}