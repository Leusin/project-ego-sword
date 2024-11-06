using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectEgoSword
{
    public class HumanoidSMBIdle : SceneLinkedSMB<HumanoidController>
    {
        override public void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector2 move = PlayerInputController.Instance.MoveInput;

            if (move.magnitude > 0)
            {
                Quaternion rotate = Quaternion.identity;

                if (move.x > 0)
                {
                    rotate = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (move.x < 0)
                {
                    rotate = Quaternion.Euler(0f, -90f, 0f);
                }

                _monobehaviour.transform.Translate(Vector3.forward * Time.deltaTime * _monobehaviour.moveSpeed);
                _monobehaviour.transform.rotation = rotate;
                animator.SetBool(_monobehaviour.hashMove, true);
            }
        }
    }
}