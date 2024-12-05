using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckAttack")]
    public class CheckAttack : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.animationProgress.attackTriggerd)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), true);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}