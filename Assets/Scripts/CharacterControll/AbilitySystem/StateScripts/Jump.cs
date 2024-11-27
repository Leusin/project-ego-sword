using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/Jump")]
    public class Jump : StateData<CharacterControl>
    {
        [Range(0f, 1f)]
        public float jumpTiming;
        public float jumpForce;
        public AnimationCurve pull;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (jumpTiming == 0f)
            {
                monobehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
                monobehaviour.animationProgress.jumped = true;
            }

            animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!monobehaviour.animationProgress.jumped && stateInfo.normalizedTime >= jumpTiming)
            {
                monobehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
                monobehaviour.animationProgress.jumped = true;
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.pullMultiplier = 0f;
            monobehaviour.animationProgress.jumped = false;
        }
    }
}