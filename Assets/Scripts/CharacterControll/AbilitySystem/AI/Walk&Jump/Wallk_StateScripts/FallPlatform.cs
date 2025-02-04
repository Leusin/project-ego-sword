using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AI/FallPlatform")]
    public class FallPlatform : StateData<CharacterControl>
    {

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 endPos = monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position;
            if (currentPos.z < endPos.z)
            {
                monobehaviour.FaceForward(true);
            }
            else if (currentPos.z > endPos.z)
            {
                monobehaviour.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 currentPos = monobehaviour.transform.position;
            Vector3 endPos = monobehaviour.aiProgress.pathFindingAgent.endSphere.transform.position;
            if (monobehaviour.IsFacingForward())
            {
                if (currentPos.z < endPos.z)
                {
                    monobehaviour.moveRight = true;
                    monobehaviour.moveLeft = false;
                }
                else
                {
                    monobehaviour.moveRight = false;
                    monobehaviour.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
            else
            {
                if (currentPos.z > endPos.z)
                {
                    monobehaviour.moveRight = false;
                    monobehaviour.moveLeft = true;
                }
                else
                {
                    monobehaviour.moveRight = false;
                    monobehaviour.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}