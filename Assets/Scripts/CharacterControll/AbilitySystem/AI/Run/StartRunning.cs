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
            Vector3 startPos = monobehaviour.aiProgress.pathfindingAgent.startSphere.transform.position;
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 directionToStart = startPos - currentPos;

            Debug.DrawLine(startPos, currentPos, Color.yellow);
            if (directionToStart.z > 0f)
            {
                monobehaviour.FaceForward(true);
                monobehaviour.moveRight = true;
                monobehaviour.moveLeft = false;
            }
            else
            {
                monobehaviour.FaceForward(false);
                monobehaviour.moveRight = false;
                monobehaviour.moveLeft = true;
            }

            float rushTriggerDistance = 2f;
            if(Vector3.SqrMagnitude(directionToStart) > rushTriggerDistance)
            {
                monobehaviour.rash = true;

            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 startPos = monobehaviour.aiProgress.pathfindingAgent.startSphere.transform.position;
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