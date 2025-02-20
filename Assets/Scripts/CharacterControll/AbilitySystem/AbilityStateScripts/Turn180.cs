using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/Turn180")]
    public class Turn180 : StateData<CharacterControl>
    {
        public bool onStart;
        public bool onEnd;

        bool _firstFrameHappened;
        bool _lastFrameHappened;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _firstFrameHappened = false;
            _lastFrameHappened = false;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onStart)
            {
                if (animator.IsInTransition(layerIndex) && 
                    !_lastFrameHappened && !_firstFrameHappened)
                {
                    _firstFrameHappened = true;

                    if (monobehaviour.IsFacingForward())
                    {
                        monobehaviour.FaceForward(false);
                        //Debug.Log("[onStart] 왼쪽   <---");
                    }
                    else
                    {
                        monobehaviour.FaceForward(true);
                        //Debug.Log("[onStart] 오른쪽   --->");
                    }
                }
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onEnd)
            {
                if (animator.IsInTransition(layerIndex) && 
                    !_lastFrameHappened && _firstFrameHappened)
                {
                    _lastFrameHappened = true;

                    if (monobehaviour.IsFacingForward())
                    {
                        monobehaviour.FaceForward(false);
                        Debug.Log("[onEnd] 왼쪽   <---");
                    }
                    else
                    {
                        monobehaviour.FaceForward(true);
                        Debug.Log("[onEnd] 오른쪽   --->");
                    }
                }
            }
        }
    }
}