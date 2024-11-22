using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckAttack")]
    public class CheckAttack : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monoBehaviour.attack && !animator.GetBool(CharacterControl.TransitionParameter.Attack.ToString()))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), true);
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}