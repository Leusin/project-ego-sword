using UnityEngine;


namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/GroundDetector")]
    public class GroundDetector : StateData<CharacterControl>
    {
        public float distance;

        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsGrounded(monoBehaviour))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), true);
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
            }

        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // -----

        private bool IsGrounded(CharacterControl monobehaviour)
        {
            if(monobehaviour.contactPoints != null)
            {
                foreach (ContactPoint contact in monobehaviour.contactPoints)
                {
                    float colliderBottom = (monobehaviour.transform.position.y + monobehaviour.boxCollider.center.y)
                        - (monobehaviour.boxCollider.size.y / 2f);
                    float yDifference = Mathf.Abs(contact.point.y - colliderBottom);

                    const float groundContactTolerance = 0.01f;
                    if (yDifference < groundContactTolerance)
                    {
                        const float groundedVelocityThreshold = 0.001f;
                        if (Mathf.Abs(monobehaviour.RigidbodyComponent.linearVelocity.y) < groundedVelocityThreshold)
                        {
                            return true;
                        }
                    }
                }
            }

            if (monobehaviour.RigidbodyComponent.linearVelocity.y < 0f)
            {
                foreach (GameObject obj in monobehaviour.bottomSpheres)
                {
                    Debug.DrawRay(obj.transform.position, -Vector3.up * distance, Color.yellow);

                    if (Physics.Raycast(obj.transform.position, -Vector3.up, out RaycastHit hit, distance))
                    {
                        if (!monobehaviour.ragdollParts.Contains(hit.collider) &&
                            !Ledge.IsLedge(hit.collider.gameObject) &&
                            !Ledge.IsLedgeChecker(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
