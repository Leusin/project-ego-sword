using System.Threading;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/CheckMove")]
    public class CheckMovement : StateData<CharacterControl>
    {
        // 캐릭터 이동 입력이 버튼이 아닌 조이스틱이라서
        // (부드러운 이동을 위해) 이동 취소의 딜레이 타이머
        private float _timer;
        private float _moveCancelDelay = -0.1f;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.moveLeft || monobehaviour.moveRight)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
                _timer = 0f;
                Debug.Log("Chehck Movement Timer Init");
            }
            else //if (_timer > _moveCancelDelay)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
                _timer = 0f;
                Debug.Log("Chehck Movement Timer Update Stop");
            }
            //else
            {
                _timer += Time.deltaTime;
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}