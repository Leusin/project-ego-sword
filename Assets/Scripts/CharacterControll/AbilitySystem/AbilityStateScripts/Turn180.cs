using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/Turn180")]
    public class Turn180 : StateData<CharacterControl>
    {
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(onStart)
            {
                if (monobehaviour.IsFacingForward())
                {
                    monobehaviour.FaceForward(false);
                }
                else
                {
                    monobehaviour.FaceForward(true);
                }
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onEnd)
            {
                if (monobehaviour.IsFacingForward())
                {
                    monobehaviour.FaceForward(false);
                }
                else
                {
                    monobehaviour.FaceForward(true);
                }
            }
        }
    }
}