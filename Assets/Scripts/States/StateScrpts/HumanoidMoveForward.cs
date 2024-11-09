using Unity.VisualScripting;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidMoveForward")]
    public class HumanoidMoveForward : StateData<HumanoidController>
    {
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;

        private bool _self;

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
            InputController controller = InputController.Instance;

            if (controller.JumpInput)
            {
                animator.SetBool(monoBehaviour.hashJump, true);
                return;
            }

            Vector2 move = controller.MoveInput;

            if (move.sqrMagnitude > 0f)
            {
                if (!CheckFront(monoBehaviour))
                {
                    Quaternion rotate = Quaternion.identity;

                    if (move.x > 0)
                        rotate = Quaternion.Euler(0f, 90f, 0f);
                    else if (move.x < 0)
                        rotate = Quaternion.Euler(0f, -90f, 0f);

                    monoBehaviour.transform.Translate(Vector3.forward * Time.deltaTime * speed * speedGraph.Evaluate(stateInfo.normalizedTime));
                    monoBehaviour.transform.rotation = rotate;
                }
            }
            else
            {
                animator.SetBool(monoBehaviour.hashMove, false);
            }
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        // -----

        private bool CheckFront(HumanoidController monoBehaviour)
        {
            if (monoBehaviour.RigidbodyComponent.linearVelocity.y < 0f)
            {
                foreach (GameObject obj in monoBehaviour.FrontSpheres)
                {
                    _self = false;

                    Debug.DrawRay(obj.transform.position, monoBehaviour.transform.forward * 0.3f, Color.yellow);
                    RaycastHit hit;
                    if (Physics.Raycast(obj.transform.position, monoBehaviour.transform.forward, out hit, blockDistance))
                    {
                        foreach (Collider c in monoBehaviour.RagdollParts)
                        {
                            if (c.gameObject == hit.collider.gameObject)
                            {
                                _self = true;
                                break;
                            }
                        }

                        if (!_self)
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