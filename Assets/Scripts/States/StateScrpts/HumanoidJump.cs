using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidJump")]
    public class HumanoidJump : StateData<HumanoidController>
    {
        public float jumpForce;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            monoBehaviour.RigidbodyComponent.AddForce(Vector3.up * jumpForce);
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}