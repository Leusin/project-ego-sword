using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/HumanoidMoveForward")]
    public class HumanoidMoveForward : StateData<HumanoidController>
    {
        public float speed;

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator)
        {
            Vector2 move = InputManager.Instance.MoveInput;

            if (move.magnitude > 0)
            {
                Quaternion rotate = Quaternion.identity;

                if (move.x > 0) 
                    rotate = Quaternion.Euler(0f, 90f, 0f);
                else if (move.x < 0)
                    rotate = Quaternion.Euler(0f, -90f, 0f);

                monoBehaviour.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                monoBehaviour.transform.rotation = rotate;
            }
            else
            {
                animator.SetBool(monoBehaviour.hashMove, false);
            }
        }
    }
}