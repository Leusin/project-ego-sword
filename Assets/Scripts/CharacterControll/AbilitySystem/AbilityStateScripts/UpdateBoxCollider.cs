using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData<CharacterControl>
    {
        public Vector3 targetCenter;
        public float centeUpdateSpeed;

        [Space(10)]
        public Vector3 targetSize;
        public float sizeUpdateSpeed;

        [Space(10)]
        public bool keepUpdating;


        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.animationProgress.updatingBoxCollider = true;

            monobehaviour.animationProgress.targetSize = targetSize;
            monobehaviour.animationProgress.sizeSpeed = sizeUpdateSpeed;
            monobehaviour.animationProgress.targetCenter = targetCenter;
            monobehaviour.animationProgress.centerSpeed = centeUpdateSpeed;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!keepUpdating)
            {
                monobehaviour.animationProgress.updatingBoxCollider = false;
            }

        }
    }
}