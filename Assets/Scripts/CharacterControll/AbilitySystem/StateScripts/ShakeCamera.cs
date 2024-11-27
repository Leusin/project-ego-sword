using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData<CharacterControl>
    {
        [Range(0f, 0.99f)]
        public float shakeTiming;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (shakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(0.2f);
                monobehaviour.animationProgress.cameraShaken = true;
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!monobehaviour.animationProgress.cameraShaken)
            {
                if (stateInfo.normalizedTime >= shakeTiming)
                {
                    CameraManager.Instance.ShakeCamera(0.2f);
                    monobehaviour.animationProgress.cameraShaken = true;
                }
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.animationProgress.cameraShaken = false;
        }
    }
}