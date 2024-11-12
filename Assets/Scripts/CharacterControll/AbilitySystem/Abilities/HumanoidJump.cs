using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidJump")]
    public class HumanoidJump : StateData<HumanoidController>
    {
        public float jumpForce;
        public AnimationCurve gravity;
        public AnimationCurve pull;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
            animator.SetBool(monoBehaviour.hashGrounded, false);
        }

        public override void UpdateAbility(HumanoidController monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.gravityMultiplier = gravity.Evaluate(stateInfo.normalizedTime);
            monobehaviour.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}