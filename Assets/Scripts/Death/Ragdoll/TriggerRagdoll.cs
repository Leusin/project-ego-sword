using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/Death/TriggerRagdoll")]
    public class TriggerRagdoll : StateData<CharacterControl>
    {
        public float triggerTiming;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > triggerTiming)
            {
                if (!monobehaviour.animationProgress.ragdollTriggerd)
                {
                    //monobehaviour.TurnOnRagdoll();
                    if(monobehaviour.skinnedMeshAnimator.enabled)
                    {
                        monobehaviour.animationProgress.ragdollTriggerd = true;
                    }
                }

            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //monobehaviour.animationProgress.ragdollTriggerd = false;
        }
    }
}