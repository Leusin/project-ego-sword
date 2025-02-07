using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/AITransitionCondition")]
    public class AITransitionCondition : StateData<CharacterControl>
    {
        public enum AITransitionType
        {
            RunToWalk,
        }

        public AITransitionType aiTransitionCondition;
        public AIType next;

        public float runToWalckStartDistance = 2f;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(TransitionToNextAI(monobehaviour))
            {
                monobehaviour.aiController.TriggerAI(next);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        bool TransitionToNextAI(CharacterControl monobehaviour)
        {
            if (aiTransitionCondition == AITransitionType.RunToWalk) 
            {
                Vector3 startPos = monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position;
                Vector3 currentPos = monobehaviour.transform.position;
                Vector3 directionToStart = startPos - currentPos;

                if(Vector3.SqrMagnitude(directionToStart) < runToWalckStartDistance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}