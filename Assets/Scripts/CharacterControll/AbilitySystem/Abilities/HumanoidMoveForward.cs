using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidMoveForward")]
    public class HumanoidMoveForward : StateData<HumanoidController>
    {
        public bool constant;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monoBehaviour.jump)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
                return;
            }

            if (constant)
            {
                ConstantMove(monoBehaviour, animator, stateInfo);
            }
            else
            {
                ControllMove(monoBehaviour, animator, stateInfo);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // -----

        private void ConstantMove(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(monoBehaviour))
            {
                monoBehaviour.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControllMove(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (monoBehaviour.move.sqrMagnitude > 0f)
            {
                if (!CheckFront(monoBehaviour))
                {
                    Quaternion rotate = Quaternion.identity;

                    if (monoBehaviour.move.x > 0)
                    {
                        rotate = Quaternion.identity;
                    }
                    else if (monoBehaviour.move.x < 0)
                    {
                        rotate = Quaternion.Euler(0f, 180f, 0f);
                    }

                    monoBehaviour.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                    monoBehaviour.transform.rotation = rotate;
                }
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
            }
        }

        private bool CheckFront(HumanoidController monoBehaviour)
        {
            if (monoBehaviour.RigidbodyComponent.linearVelocity.y < 0f)
            {
                foreach (GameObject obj in monoBehaviour.frontSpheres)
                {
                    Debug.DrawRay(obj.transform.position, monoBehaviour.transform.forward * 0.3f, Color.yellow);
                    RaycastHit hit;
                    if (Physics.Raycast(obj.transform.position, monoBehaviour.transform.forward, out hit, blockDistance))
                    {
                        if (!monoBehaviour.ragdollParts.Contains(hit.collider))
                        {
                            if (IsBodyPart(hit.collider))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool IsBodyPart(Collider collider)
        {
            HumanoidController control = collider.GetComponent<HumanoidController>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == collider.gameObject)
            {
                return false;
            }

            if (control.ragdollParts.Contains(collider))
            {
                return true;
            }

            return false;
        }
    }
}