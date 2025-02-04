using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/StartRunning")]
    public class StartRunning : StateData<CharacterControl>
    {
        private float _targetDistanceThreshold = 1f;
        private float _jumpDistanceThreshold = 0.01f;
        private float _straightDistanceThreshold = 2.0f;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 startPos = monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position;
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 dir = startPos - currentPos;

            Debug.DrawLine(startPos, currentPos, Color.yellow);
            if (dir.z > 0f)
            {
                monobehaviour.moveRight = true;
                monobehaviour.moveLeft = false;
            }
            else
            {
                monobehaviour.moveRight = false;
                monobehaviour.moveLeft = true;
            }

            monobehaviour.rash = true;
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 startPos = monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position;
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 directionToStart = startPos - currentPos;

            if (Vector3.SqrMagnitude(directionToStart) < _straightDistanceThreshold)
            {
                monobehaviour.moveRight = false;
                monobehaviour.moveLeft = false;
                monobehaviour.rash = false;
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}