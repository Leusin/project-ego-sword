using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidIdle")]
    public class HumanoidIdle : StateData<HumanoidController>
    {
        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator)
        {
            Vector2 move = InputController.Instance.MoveInput;

            if (move.magnitude > 0)
            {
                animator.SetBool(monoBehaviour.hashMove, true);
            }
        }
    }
}