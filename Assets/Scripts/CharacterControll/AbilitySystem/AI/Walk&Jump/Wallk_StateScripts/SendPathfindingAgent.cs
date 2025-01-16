using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public enum AI_Walk_Transitions
    {
        StartWalk,
        JumpPlatform,
        FallPlatform,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(monobehaviour.aiProgress.pathFindingAgent == null)
            {
                GameObject p =Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject)) as GameObject);
                monobehaviour.aiProgress.pathFindingAgent = p.GetComponent<PathFindingAgent>();
            }

            monobehaviour.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            monobehaviour.aiProgress.pathFindingAgent.transform.position = monobehaviour.transform.position;
            monobehaviour.aiProgress.pathFindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(monobehaviour.aiProgress.pathFindingAgent.startWalk)
            {
                animator.SetBool(AI_Walk_Transitions.StartWalk.ToString(), true);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AI_Walk_Transitions.StartWalk.ToString(), false);
        }
    }
}