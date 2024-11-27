using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/MoveForward")]
    public class MoveForward : StateData<CharacterControl>
    {
        public bool constant;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.jump)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
                return;
            }

            if (constant)
            {
                ConstantMove(monobehaviour, animator, stateInfo);
            }
            else
            {
                ControllMove(monobehaviour, animator, stateInfo);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // -----

        private void ConstantMove(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(monobehaviour))
            {
                monobehaviour.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControllMove(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
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

        private bool CheckFront(CharacterControl monoBehaviour)
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
                            if (!IsBodyPart(hit.collider) && !Ledge.IsLedge(hit.collider.gameObject))
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
            HumanoidControl control = collider.GetComponent<HumanoidControl>();

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