using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/ResetLocalPosition")]
    public class ResetLocalPosition : StateData<CharacterControl>
    {
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onStart)
            {
                monobehaviour.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                monobehaviour.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex1)
        {
            if (onEnd)
            {
                monobehaviour.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                monobehaviour.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}