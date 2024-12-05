using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/DisallowEarlyTurn")]
    public class DisallowEarlyTurn : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.animationProgress.disallowEarylTurn = true;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}