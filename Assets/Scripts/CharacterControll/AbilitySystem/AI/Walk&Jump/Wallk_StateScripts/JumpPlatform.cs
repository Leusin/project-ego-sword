using Unity.VisualScripting;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/JumpPlatform")]
    public class JumpPlatform : StateData<CharacterControl>
    {
        private float _jumpTriggerDistance = 1.5f;
        private float _distToTargetHight = 0.001f;

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.jump = true;
            monobehaviour.moveUp = true;

            if (monobehaviour.aiProgress.pathFindingAgent.startSphere.transform.position.z <
                monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position.z)
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
            Vector3 bottom = monobehaviour.frontSpheres[0].transform.position;
            Vector3 top = monobehaviour.frontSpheres[0].transform.position;
            Vector3 target = monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position;

            float topDist = target.y - top.y;
            float bottiomhightDist = target.y - bottom.y;

            if (topDist < _jumpTriggerDistance)
            {
                if (monobehaviour.IsFacingForward())
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


            Debug.DrawLine(bottom, target, Color.red);
            if (bottiomhightDist < _distToTargetHight)
            {
                monobehaviour.jump = false;
                monobehaviour.moveRight = false;
                monobehaviour.moveLeft = false;
                //monobehaviour.moveUp = false; // 점프중에 목표에 다다르면 파쿠르를 하지 않아 제외

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monobehaviour.moveUp = false;
        }
    }
}