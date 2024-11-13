using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/Jump")]
    public class Jump : StateData<CharacterControl>
    {
        public float jumpForce;
        public AnimationCurve gravity;
        public AnimationCurve pull;

        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
            animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.gravityMultiplier = gravity.Evaluate(stateInfo.normalizedTime);
            monobehaviour.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}