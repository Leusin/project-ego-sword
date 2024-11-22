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
        private bool isJumped;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (jumpTiming == 0f)
            {
                monobehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }

            animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!isJumped && stateInfo.normalizedTime >= jumpTiming)
            {
                monobehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.pullMultiplier = 0f;
            isJumped = false;
        }
    }
}