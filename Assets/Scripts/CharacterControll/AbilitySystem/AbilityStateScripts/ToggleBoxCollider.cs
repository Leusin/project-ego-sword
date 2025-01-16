using UnityEngine;

namespace ProjectEgoSword
{
    /// <summary>
    /// 원하지 않는 객체의 충돌체까지 동작되는 버그가 있다.
    /// 가령, 플레이어가 조작하는 캐릭터 파쿠르를 할때 다른 더미 캐릭터의 박스 콜라이더가 조작된다.
    /// 원인을 찾기 전까지 가능한 사용하지 않는게 좋켔다.(2025-11-16)
    /// </summary>
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData<CharacterControl>
    {
        public bool on;
        public bool onStart;
        public bool onEnd;

        private BoxCollider _boxCollider;

        public override void OnStart(CharacterControl monoBehaviour, Animator animator)
        {
            _boxCollider = monoBehaviour.GetComponent<BoxCollider>();
        }

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onStart)
            {
                ToggleBoxCol(monobehaviour);
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onEnd)
            {
                ToggleBoxCol(monobehaviour);
            }
        }

        // -----

        private void ToggleBoxCol(CharacterControl monobehaviour)
        {
            _boxCollider.enabled = on;
        }
    }
}