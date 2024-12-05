using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/ToggleGravity")]
    public class ToggleGravity : StateData<CharacterControl>
    {
        public bool on;
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(onStart)
            {
                ToggleGrav(monobehaviour);
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onEnd)
            {
                ToggleGrav(monobehaviour);
            }
        }

        // -----

        private void ToggleGrav(CharacterControl monobehaviour)
        {
            monobehaviour.RigidbodyComponent.linearVelocity = Vector3.zero;
            monobehaviour.RigidbodyComponent.useGravity = on;
        }
    }
}
