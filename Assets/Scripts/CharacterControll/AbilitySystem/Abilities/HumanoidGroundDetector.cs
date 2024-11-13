using UnityEngine;


namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidGroundDetector")]
    public class HumanoidGroundDetector : StateData<HumanoidController>
    {
        [Range(0.01f, 1f)]
        public float checkTime;
        public float distance;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= checkTime)
            {
                bool isGrounded = IsGrounded(monoBehaviour);
                animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), isGrounded);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // -----

        private bool IsGrounded(HumanoidController monoBehaviour)
        {
            if (monoBehaviour.RigidbodyComponent.linearVelocity.y > -0.001f &&
                monoBehaviour.RigidbodyComponent.linearVelocity.y <= 0f)
            {
                return true;
            }

            if (monoBehaviour.RigidbodyComponent.linearVelocity.y < 0f)
            {
                foreach (GameObject obj in monoBehaviour.bottomSpheres)
                {
                    Debug.DrawRay(obj.transform.position, -Vector3.up * 0.7f, Color.yellow);

                    RaycastHit hit;
                    if (Physics.Raycast(obj.transform.position, -Vector3.up, out hit, distance))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
