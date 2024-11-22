using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/GravityPull")]
    public class GravityPull : StateData<CharacterControl>
    {
        public AnimationCurve gravity;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.pullMultiplier = gravity.Evaluate(stateInfo.normalizedTime);
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.gravityMultiplier = 0f;
        }
    }
}
