using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameObject anim = monobehaviour.skinnedMeshAnimator.gameObject;
            anim.transform.parent = monobehaviour.ledgeChecker.grabbingLedge.transform;
            anim.transform.localPosition = monobehaviour.ledgeChecker.grabbingLedge.offeset;

            monobehaviour.RigidbodyComponent.linearVelocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}