using UnityEngine;

namespace ProjectEgoSword
{
    /// <summary>
    /// ������ �ʴ� ��ü�� �浹ü���� ���۵Ǵ� ���װ� �ִ�.
    /// ����, �÷��̾ �����ϴ� ĳ���� ������ �Ҷ� �ٸ� ���� ĳ������ �ڽ� �ݶ��̴��� ���۵ȴ�.
    /// ������ ã�� ������ ������ ������� �ʴ°� ���N��.(2025-11-16)
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