using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData<CharacterControl>
    {
        [Range(0f, 0.99f)]
        public float shakeTiming;
        private bool isShaken;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (shakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(0.2f);
                isShaken = true;
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!isShaken)
            {
                if (stateInfo.normalizedTime >= shakeTiming)
                {
                    CameraManager.Instance.ShakeCamera(0.2f);
                    isShaken = true;
                }
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            isShaken = false;
        }
    }
}