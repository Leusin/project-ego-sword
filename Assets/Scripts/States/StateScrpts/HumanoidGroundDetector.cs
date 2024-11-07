using UnityEngine;


namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidGroundDetector")]
    public class HumanoidGroundDetector : StateData<HumanoidController>
    {
        public float checkTime;
        public float distance;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            bool isGrounded = IsGrounded(monoBehaviour);
            animator.SetBool(monoBehaviour.hashGrounded, isGrounded);
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        // -----

        private bool IsGrounded(HumanoidController monoBehaviour)
        {
            if (monoBehaviour.RigidbodyComponent.linearVelocity.y > -0.01f &&
                monoBehaviour.RigidbodyComponent.linearVelocity.y <= 0f)
            {
                return true;
            }

            foreach (GameObject obj in monoBehaviour.BottomSpheres)
            {
                Debug.DrawRay(obj.transform.position, -Vector3.up * 0.7f, Color.yellow);

                RaycastHit hit;
                if (Physics.Raycast(obj.transform.position, -Vector3.up, out hit, distance))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
