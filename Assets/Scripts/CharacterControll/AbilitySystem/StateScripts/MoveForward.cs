using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/MoveForward")]
    public class MoveForward : StateData<CharacterControl>
    {
        public bool allowEarlyTurn;
        public bool lockDriection;
        public bool constant;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(allowEarlyTurn && !monobehaviour.animationProgress.disallowEarylTurn)
            {
                if(monobehaviour.moveLeft)
                {
                    monobehaviour.FaceForward(false);
                }
                else if(monobehaviour.moveRight)
                {
                    monobehaviour.FaceForward(true);
                }
            }

            monobehaviour.animationProgress.disallowEarylTurn = false;
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

        private void ControllMove(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (monobehaviour.moveLeft || monobehaviour.moveRight)
            {
                if (!CheckFront(monobehaviour))
                {
                    monobehaviour.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
            }

            CheckTurn(monobehaviour);
        }

        private void CheckTurn(CharacterControl monobehaviour)
        {
            if (!lockDriection)
            {
                if (monobehaviour.moveRight)
                {
                    monobehaviour.transform.rotation = Quaternion.identity;
                }
                else if (monobehaviour.moveLeft)
                {
                    monobehaviour.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
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
                            if (!IsBodyPart(hit.collider) &&
                                !Ledge.IsLedge(hit.collider.gameObject) &&
                                !Ledge.IsLedgeChecker(hit.collider.gameObject))
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