using UnityEngine;

namespace ProjectEgoSword
{
    public class HumanoidSMBWalking : SceneLinkedSMB<HumanoidController>
    {
        override public void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector2 move = PlayerInputController.Instance.MoveInput;

            if (move.magnitude > 0 == false)
            {
                animator.SetBool(_monobehaviour.hashMove, false);
            }
        }
    }
}