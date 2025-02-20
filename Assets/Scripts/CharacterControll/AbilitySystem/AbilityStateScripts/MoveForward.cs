using UnityEngine;

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

        [Header("Momentum")]
        public float startingMomentum;
        public bool useMomentum;
        public float maxMomentum;
        public bool clearMomentumOnExit;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (allowEarlyTurn && !monobehaviour.animationProgress.disallowEarylTurn)
            {
                if (monobehaviour.moveLeft)
                {
                    monobehaviour.FaceForward(false);
                }
                else if (monobehaviour.moveRight)
                {
                    monobehaviour.FaceForward(true);
                }
            }

            monobehaviour.animationProgress.disallowEarylTurn = false;

            float tolerance = 0.001f;
            if(startingMomentum > tolerance)
            {
                if(monobehaviour.IsFacingForward())
                {
                    monobehaviour.animationProgress.airMomentum = startingMomentum;
                }
                else
                {
                    monobehaviour.animationProgress.airMomentum = -startingMomentum;
                }
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 2025-01-16
            // AI 가 점프하면서 앞으로 나아가지 못해서 주석처리했다.
            // 추후 아래 코드가 필요하지 않는다면 완전히 지울 것.
            //
            //if (monobehaviour.jump)
            //{
            //    animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
            //    return;
            //}

            if (monobehaviour.rash)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), true);
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Sprint.ToString(), false);
            }

            if (useMomentum)
            {
                UpdateOnMomentum(monobehaviour, animator, stateInfo);
            }
            else if (constant)
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
            if (clearMomentumOnExit)
            {
                monobehaviour.animationProgress.airMomentum = 0f;
            }
        }

        // -----

        private void UpdateOnMomentum(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(monobehaviour.animationProgress.frameUpdated)
            {
                return;
            }

            monobehaviour.animationProgress.frameUpdated = true;

            if (monobehaviour.moveRight)
            {
                monobehaviour.animationProgress.airMomentum += speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }

            if (monobehaviour.moveLeft)
            {
                monobehaviour.animationProgress.airMomentum -= speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }

            if (Mathf.Abs(monobehaviour.animationProgress.airMomentum) >= maxMomentum)
            {
                if (monobehaviour.animationProgress.airMomentum > 0f)
                {
                    monobehaviour.animationProgress.airMomentum = maxMomentum;
                }
                else if (monobehaviour.animationProgress.airMomentum < 0f)
                {
                    monobehaviour.animationProgress.airMomentum = -maxMomentum;
                }
            }

            if (monobehaviour.animationProgress.airMomentum > 0f)
            {
                monobehaviour.FaceForward(true);
            }
            else if (monobehaviour.animationProgress.airMomentum < 0f)
            {
                monobehaviour.FaceForward(false);
            }

            if (!IsBlocked(monobehaviour))
            {
                monobehaviour.MoveForward(speed, Mathf.Abs(monobehaviour.animationProgress.airMomentum));
            }
        }

        private void ConstantMove(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsBlocked(monobehaviour))
            {
                monobehaviour.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControllMove(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (monobehaviour.moveLeft || monobehaviour.moveRight)
            {
                if (!IsBlocked(monobehaviour))
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

        private bool IsBlocked(CharacterControl monoBehaviour)
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