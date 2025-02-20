using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public enum AI_Walk_Transitions
    {
        StartWalk,
        JumpPlatform,
        FallPlatform,
        StartRun,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(monobehaviour.aiProgress.pathfindingAgent == null)
            {
                GameObject p =Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject)) as GameObject);
                monobehaviour.aiProgress.pathfindingAgent = p.GetComponent<PathFindingAgent>();
            }


            monobehaviour.aiProgress.pathfindingAgent.owner = monobehaviour;
            monobehaviour.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            monobehaviour.aiProgress.pathfindingAgent.transform.position = monobehaviour.transform.position;
            monobehaviour.navMeshObstacle.carving = false;
            monobehaviour.aiProgress.pathfindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(monobehaviour.aiProgress.pathfindingAgent.startWalk)
            {
                animator.SetBool(AI_Walk_Transitions.StartWalk.ToString(), true);
                animator.SetBool(AI_Walk_Transitions.StartRun.ToString(), true);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AI_Walk_Transitions.StartWalk.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.StartRun.ToString(), false);
        }
    }
}