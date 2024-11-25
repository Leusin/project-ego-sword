using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 endPosition = monoBehaviour.ledgeChecker.grabbingLedge.transform.position + monoBehaviour.ledgeChecker.grabbingLedge.endPosition;

            monoBehaviour.transform.position = endPosition;
            monoBehaviour.skinnedMeshAnimator.transform.position = endPosition;
            monoBehaviour.skinnedMeshAnimator.transform.parent = monoBehaviour.transform;
        }
    }
}