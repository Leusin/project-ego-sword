using Unity.VisualScripting;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/JumpPlatform")]
    public class JumpPlatform : StateData<CharacterControl>
    {
        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.jump = true;
            monobehaviour.moveUp = true;

            if (monobehaviour.aiProgress.pathfindingAgent.startSphere.transform.position.z <
                monobehaviour.aiProgress.pathfindingAgent.endSphere.transform.position.z)
            {
                monobehaviour.FaceForward(true);
            }
            else
            {
                monobehaviour.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(monobehaviour.attack)
            {
                return;
            }

            float platformDist = monobehaviour.aiProgress.pathfindingAgent.endSphere.transform.position.y -
               monobehaviour.frontSpheres[0].transform.position.y;

            float jumpTriggerDistance = 0.5f;
            if (platformDist > jumpTriggerDistance)
            {
                if (monobehaviour.aiProgress.pathfindingAgent.startSphere.transform.position.z <
                monobehaviour.aiProgress.pathfindingAgent.endSphere.transform.position.z)
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
            else
            //if (platformDist < jumpTriggerDistance)
            {
                monobehaviour.jump = false;
                monobehaviour.moveRight = false;
                monobehaviour.moveLeft = false;
                monobehaviour.moveUp = false; // 점프중에 목표에 다다르면 파쿠르를 하지 않아 제외

                // TEMP
                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}