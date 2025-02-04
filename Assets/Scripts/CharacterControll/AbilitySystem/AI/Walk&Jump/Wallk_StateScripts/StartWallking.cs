using UnityEngine;
using static CharacterControl;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/StartWallking")]
    public class StartWallking : StateData<CharacterControl>
    {
        private float _targetDistanceThreshold = 1f;
        private float _jumpDistanceThreshold = 0.01f;
        private float _straightDistanceThreshold = 0.5f;

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
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 startPos = monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position;
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 directionToStart = startPos - currentPos;

            Debug.DrawLine(startPos, currentPos, Color.green);

            // Straight 직진
            if (monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position.y ==
                monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(directionToStart) < _straightDistanceThreshold)
                {
                    monobehaviour.moveRight = false;
                    monobehaviour.moveLeft = false;

                    Vector3 targetDist = currentPos - monobehaviour.aiProgress.pathFindingAgent.target.transform.position;
                    if (targetDist.sqrMagnitude > _targetDistanceThreshold)
                    {
                        // 임시 조치. 추후 변경 예정(2025-01-16)
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                }
                // TAMP: 공격 AI 나중에 수정할 것.
                else
                {
                    if (CharacterManager.Instance.GetPlayerbleCharacter().
                        damageDetector.damageTaken == 0)
                    {
                        if (monobehaviour.IsFacingForward())
                        {
                            monobehaviour.moveRight = true;
                            monobehaviour.moveLeft = false;
                            //monobehaviour.attack = true;
                            monobehaviour.skinnedMeshAnimator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), 1);
                        }
                        else
                        {
                            monobehaviour.moveRight = false;
                            monobehaviour.moveLeft = true;
                            //monobehaviour.attack = true;
                            monobehaviour.skinnedMeshAnimator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), 1);
                        }
                    }
                }
            }
            // Jump
            else if (monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position.y <
                monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(directionToStart) < _jumpDistanceThreshold)
                {
                    monobehaviour.moveRight = false;
                    monobehaviour.moveLeft = false;

                    animator.SetBool(AI_Walk_Transitions.JumpPlatform.ToString(), true);
                }
            }
            // Fall
            else
            {
                animator.SetBool(AI_Walk_Transitions.FallPlatform.ToString(), true);
            }

        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AI_Walk_Transitions.JumpPlatform.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.FallPlatform.ToString(), false);
        }
    }
}